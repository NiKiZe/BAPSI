
using System.Security.Cryptography;
using System.Text;

namespace BapsiNet.Packet;

public class BapsiPacket
{
    /// <summary>Raw on wire packet</summary>
    private readonly ReadOnlyMemory<byte> _packet;

    /// <summary>P:0 Start <SOH> 1B Start bit-pattern (0x01) E:No H:No</summary>
    public byte Start0 => _packet.Span[0];
    /// <summary>P:1 Start <SOH> 1B Start bit-pattern (0x01) E:No H:No</summary>
    public byte Start1 => _packet.Span[1];
    /// <summary>P:2 Version 1B Implemented BAPSI-version E:No H:No</summary>
    public int Version => _packet.Span[2];
    /// <summary>P:3 Encryption 1B Type of encryption 0x00=Unencrypted, 0x01=RC4 type E:No H:No</summary>
    /// <remarks>
    /// The only message that may be unencrypted, <see cref="EncryptionType.Unencrypted"/>, is
    /// Acknowledge (C: 0x61 T: 0x06) with response code 310, Server encryption error.
    /// </remarks>
    public EncryptionType Encryption => (EncryptionType)_packet.Span[3];
    /// <summary>P:4 Crypt 4B Random crypt data E:No H:No</summary>
    public ReadOnlyMemory<byte> Crypt => _packet.Slice(4, 4);
    /// <summary>P:8 Length 2B Length of data (Short, big-endian) E:No H:No</summary>
    public ushort Length => (ushort)((_packet.Span[8] << 8) + _packet.Span[9]);

    /// <summary>P:10 MD5 Hash 16B MD5 hash of Data class, Data type and Data E:Yes H:No</summary>
    public ReadOnlyMemory<byte> Md5 { get; private set; }

    /// <summary>P:26 Data class 1B  E:Yes H:Yes</summary>
    /// <remarks>Use <see cref="DataClassAndType"/> instead</remarks>
    private DataClassType DataClass => _dataPart == null ?
        DataClassType.Undefined :
        (DataClassType)(_dataPart.Value[..1].Span[0] << 8);

    /// <summary>P:27 Data type 1B  E:Yes H:Yes</summary>
    /// <remarks>Use <see cref="DataClassAndType"/> instead</remarks>
    private byte DataType => _dataPart == null ? (byte)0x00 : _dataPart.Value.Slice(1, 1).Span[0];

    /// <summary>P:26-27 Data class 1B, Data type 1B  E:Yes H:Yes</summary>
    public DataClassType DataClassAndType => _dataPart == null ? 
        DataClassType.Undefined :
        (DataClassType)((_dataPart.Value[..2].Span[0] << 8) | _dataPart.Value[..2].Span[1]);

    /// <summary>P:28 Data items ..B TAB(ASCII-9) separated data NULL terminated E:Yes E:Yes</summary>
    public ReadOnlyMemory<byte>? DataItems => _dataPart?[2..];
    public string DataText => DataItems == null ? string.Empty : DataEncoding.GetString(DataItems.Value.Span);

    private static readonly Encoding DataEncoding = Encoding.ASCII;

    /// <summary>Pointer to encrypted data in <see cref="_packet"/></summary>
    private ReadOnlyMemory<byte> EncryptedPart => _packet[10..];

    /// <summary>Decrypted data (hashed part), including <see cref="DataClass"/> and <see cref="DataType"/> but excluding <see cref="Md5"/></summary>
    private ReadOnlyMemory<byte>? _dataPart;

    /// <summary>
    /// Create packet structure from a set of bytes
    /// </summary>
    public BapsiPacket(ReadOnlyMemory<byte> packet)
    {
        _packet = packet;
        if (Start0 != 0x01)
            throw new ArgumentOutOfRangeException(nameof(packet), packet, "Non expected SOH0");
        if (Start1 != 0x01)
            throw new ArgumentOutOfRangeException(nameof(packet), packet, "Non expected SOH1");
        if (Version != 1)
            throw new ArgumentOutOfRangeException(nameof(packet), packet, "Non expected Version");
    }

    /// <summary>
    /// Generate new data packet, similar to BAPSIDLL PutBuffer using string
    /// </summary>
    public BapsiPacket(DataClassType dataClassAndType, string data)
        : this(dataClassAndType, DataEncoding.GetBytes(data))
    {
    }

    /// <summary>
    /// Generate new data packet, similar to BAPSIDLL PutBuffer using a set of bytes
    /// </summary>
    public BapsiPacket(DataClassType dataClassAndType, ReadOnlySpan<byte> data)
    {
        // prepare static packet parts
        Memory<byte> packet = new byte[28 + data.Length];
        _packet = packet;
        var header = packet.Span;
        header[0] = 0x01; // Start0
        header[1] = 0x01; // Start1
        header[2] = 1; // Version
        header[3] = (byte)EncryptionType.Unencrypted;
        // Crypt 4 bytes set by GetCryptPacket
        // Length 2 bytes ...
        var length = (ushort)data.Length;
        header[8] = (byte)(length >> 8 & 0xff);
        header[9] = (byte)(length & 0xff);
        // MD5 calculated and set later

        // allocate data
        header[26] = (byte)((ushort)dataClassAndType >> 8 & 0xff);
        header[27] = (byte)((ushort)dataClassAndType & 0xff);
        data.CopyTo(header[28..]);
        var dPartM = packet[26..];
        _dataPart = dPartM;

        MD5.HashData(dPartM.Span, packet.Slice(10, 16).Span);
#if DEBUG // validate correct hash implemenation
        VerifyHash(packet[10..]);
#endif
    }

    /// <summary>Get encrypted packet, <see cref="Crypt"/> part of returned buffer gets a new random value</summary>
    /// <exception cref="InvalidOperationException">If not <see cref="EncryptionType.Unencrypted"/></exception>
    public ReadOnlyMemory<byte> GetCryptPacket(byte[] key)
    {
        if (Encryption != EncryptionType.Unencrypted)
            throw new InvalidOperationException("Can only get packet from unencrypted instance");

        // get a packet copy
        Memory<byte> buffer = _packet.ToArray();
        var header = buffer.Span;
        header[3] = (byte)EncryptionType.RC4;
        RandomNumberGenerator.Fill(header.Slice(4, 4));
        ApplyCiper(header[10..], key, buffer.Slice(4, 4));

#if DEBUG
        // verify packet while developing
        var bPacket = new BapsiPacket(buffer);
        bPacket.Cipher(key);
#endif

        return buffer;
    }

    /// <summary>Do the actual RC4 ciper manipulation of <paramref name="buffer"/></summary>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="key"/> is not valid</exception>
    private static void ApplyCiper(Span<byte> buffer, byte[] key, ReadOnlyMemory<byte> crypt)
    {
        if (key.Length != 16)
            throw new ArgumentOutOfRangeException(nameof(key), key, "must be 16 bytes");

        // combine key with crypt
        var rc = new RC4(key.Concat(crypt.ToArray()).ToArray());
        // apply ciper to data
        rc.Apply(buffer);
    }

    /// <summary>Apply decryption <paramref name="key"/> to packet to get decoded data and verify result</summary>
    /// <exception cref="InvalidOperationException">If MD5 dosn't match</exception>
    public ReadOnlySpan<byte> Cipher(byte[] key)
    {
        // a copy that can be manipulated
        Memory<byte> buffer = EncryptedPart.ToArray();
        if (Encryption == EncryptionType.RC4)
        {
            ApplyCiper(buffer.Span, key, Crypt);
        }

        var data = VerifyHash(buffer);
        _dataPart = data;
        return data.Span;
    }

    /// <summary>Verify hash of the encrypted data</summary>
    /// <exception cref="InvalidOperationException">If MD5 dosn't match</exception>
    private Memory<byte> VerifyHash(Memory<byte> buffer)
    {
        // get data part (excluding MD5)
        var data = buffer[16..];
        var buf = buffer.Span;
        // compute hash on data
        var calcHash = GC.AllocateUninitializedArray<byte>(128 / 8);
        MD5.HashData(data.Span, calcHash);
        // verify hash
        for (int i = 0; i < calcHash.Length; i++)
        {
            if (calcHash[i] != buf[i])
                throw new InvalidOperationException($"Invalid hash {i}: {calcHash[i]:x2} vs {buf[i]:x2}");
        }
        // we are valid, save the extracted data
        Md5 = calcHash;
        return data;
    }

    /// <summary>
    /// Get a <see cref="EncryptionType.Unencrypted"/> instance of packet,
    /// (<see cref="DataClassAndType"/> and <see cref="DataItems"/> is copied)
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If packet is <see cref="EncryptionType.RC4"/> and <paramref name="key"/> is not valid
    /// </exception>
    public BapsiPacket GetDecodedPacket(byte[] key)
    {
        if (DataItems == null)
            Cipher(key);
        var p = new BapsiPacket(DataClassAndType, DataItems.Value.Span);
        return p;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"V:{Version} E:{Encryption} L:{Length}");
        var dct = DataClassAndType;
        if (DataItems != null && dct != DataClassType.Undefined)
        {
            if (!Enum.IsDefined(dct))
                sb.Append($" C:{DataClass}");
            sb.Append($" CT:{dct}(0x{(ushort)dct:x4}) Txt:")
                .Append(DataText).Append(" D:");
            foreach (var b in DataItems.Value.Span)
            {
                if (char.IsLetterOrDigit((char)b))
                    sb.Append((char)b);
                else sb.Append($" 0x{b:X2} ");
            }
        }
        return sb.ToString();
    }
}
