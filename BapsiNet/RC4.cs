namespace BapsiNet;

/// <remarks>https://en.wikipedia.org/wiki/RC4</remarks>
public class RC4
{
    private const int MXC = 0x100; // 256

    private readonly byte[] sblock = new byte[MXC]; // The array contained S-block
    private int x = 0, y = 0;

    // Creates an RC4 instance using the given key of any length.
    public RC4(ReadOnlySpan<byte> key)
    {
        // S-block initialization
        for (int i = 0; i < MXC; i++)
        {
            sblock[i] = (byte)i;
        }

        // KSA
        for (int i = 0, j = 0, l = key.Length; i < MXC; i++)
        {
            j = (j + sblock[i] + key[i % l]) % MXC;
            Swap(sblock, i, j);
        }
    }

    // Performs encryption or decryption of the data.
    public void Apply(Span<byte> buffer)
    {
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = Convert.ToByte(buffer[i] ^ NextByte());
        }
    }

    private static void Swap(Span<byte> s, int i1, int i2) => (s[i2], s[i1]) = (s[i1], s[i2]);

    private byte NextByte() // PRGA
    {
        x = (x + 1) % MXC;
        y = (y + sblock[x]) % MXC;
        Swap(sblock, x, y);
        return sblock[(sblock[x] + sblock[y]) % MXC];
    }
}
