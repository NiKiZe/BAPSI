using BapsiNet.Packet;
using System.Text;

namespace BapsiNet.Test.Packet;

[TestFixture]
public class PacketTests
{
    public static readonly byte[] DefaultKey = {
        0x99, 0x99, 0x99, 0x99, 0x99, 0x99, 0x99, 0x99,
        0x99, 0x99, 0x99, 0x99, 0x99, 0x99, 0x99, 0x99};

    public static BapsiPacket DecodeTest(ReadOnlyMemory<byte> bytes)
    {
        return DecodeTest(new BapsiPacket(bytes), bytes);
    }

    public static BapsiPacket DecodeTest(BapsiPacket p, ReadOnlyMemory<byte>? bytes)
    {
        Console.WriteLine(p.ToString());
        if (bytes.HasValue)
            Assert.That(p.Length, Is.EqualTo(bytes.Value.Length - 1 - 1 - 1 - 1 - 4 - 2 - 16 - 1 - 1));

        var x = p.Cipher(DefaultKey);
        Console.Out.WriteHexLine(x);
        Console.WriteLine(p.ToString());
        Assert.That(x.Length, Is.EqualTo(p.Length + 1 + 1));

        return p;
    }

    [TestCase(DataClassType.SystemPing, "01 01 01 01 63 48 50 4e  00 02 54 de 1c ef 78 93  1a 92 bc 41 de 61 33 a4  ac 93 80 53 6c b5")]
    [TestCase(DataClassType.SystemLogin, "01 01 01 01 2f 33 42 9f  00 09 14 ab 18 81 09 2d  ea 43 90 19 3a 87 4b 24  b2 4c 69 67 c5 5b 60 c6 0e d3 16 bc 66")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 0d 99 a5 fc  00 02 ca 50 90 9c ff bf  92 77 e6 6d 09 e1 4b cf  ae 6b 01 80 df af")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 b5 eb 74 7f  00 02 2a 22 01 1c 33 66  50 54 2e d0 a0 fc 2b aa  c2 0c f8 ea ff 60")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 a2 44 ae 41  00 02 8d 7d dd e9 2e 3c  2c c6 20 73 29 e2 16 68  10 90 38 30 eb eb")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 5a 9f b4 2b  00 02 14 6f 9f 70 5b 61  12 fd e1 b7 ae 8c fb 1d  b5 f3 4f b7 37 1b")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 f5 48 f2 09  00 02 7d 3e 48 08 62 10  e6 db c1 1d af f5 53 0e  34 6c 59 2c e6 96")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 dc 90 c1 e0  00 02 2f d6 76 87 24 46  8c 06 86 38 e6 bd be c7  98 eb e1 6a 5f 92")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 d1 c5 8c 82  00 02 5c 28 7f 27 3d 29  34 83 2f 9f 76 5b e6 4d  b1 48 d9 4a ab 19")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 30 61 32 5f  00 02 2d 56 e0 3b ab 50  09 99 69 5a f3 2f 7d 93  8d 22 9c cd 31 43")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 74 82 ac 9e  00 02 6d c2 36 2d 30 0e  bf cc 39 6a e6 57 0a 15  b7 5e 18 73 16 30")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 fc 9a f0 6d  00 02 45 27 1b e1 f7 9a  59 da 1c 0d 84 9f 14 59  fc f9 c6 8b 22 f5")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 10 68 15 95  00 02 78 90 57 db 07 74  83 cf 56 35 ba 6c 93 c4  2e bc d0 a8 28 b9")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 1f 27 b7 51  00 02 16 bb ac f0 45 9a  fe d5 7d 70 c3 71 cd 9c  f4 a5 1e 97 88 96")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 4a 06 9d 60  00 02 b9 37 49 6f 8a 6f  68 0c 02 6d 19 49 bf 34  93 99 58 1c d4 8e")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 27 d7 99 59  00 02 51 0a fb 6f c2 35  7c 19 a5 4f c2 c6 86 ff  c1 82 25 86 63 c8")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 be 0b b0 3f  00 02 60 ab 5c f2 43 78  3f 28 9d 22 45 ee 21 16  16 fb a3 ee f2 09")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 d8 dd 7b 55  00 02 73 3e f6 0b 96 31  90 54 31 aa 86 25 8e be  62 d0 4f f6 07 01")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 7a ce cd 33  00 02 da 4c 5a b6 12 29  91 9e 99 b3 95 68 54 79  75 bc b1 ee 39 66")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 ac 52 97 1a  00 02 e8 75 72 b1 fe 36  69 30 5a f4 11 06 f1 37  62 fe 25 79 f7 eb")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 7e c9 08 85  00 02 ae 7e bd 0e 69 89  95 10 d5 b7 e5 52 20 6f  ba e2 67 86 2d 26")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 4d b0 f8 00  00 02 ef 1e c3 67 9e 4e  80 73 b6 84 f3 98 38 b2  7d cf 9d 4c ec 3a")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 43 19 86 3d  00 02 c4 d1 94 63 b7 10  74 8a f6 d6 9f 8d 2a 4c  bc 52 0b f9 f4 bd")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 1d 58 00 65  00 02 86 35 2f 4c f6 e3  cc 4b 95 8e e1 6e 8c f8  a9 87 22 50 93 9d")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 31 00 04 ac  00 02 05 68 b6 a6 97 6c  fe 31 eb 59 97 d2 17 f9  f7 dc e8 4b b0 11")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 af 0e e5 28  00 02 2e 7b e0 00 14 10  80 a3 f2 80 de a5 78 0e  5f e7 ea 36 e7 bd")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 27 63 50 e3  00 02 66 e7 e9 9a 11 b2  62 c2 ab 30 cd 24 7c 37  57 9d 10 c1 fd 30")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 4b 78 30 2f  00 02 d5 77 2b 6c dc 80  99 b3 65 6e f0 57 53 46  5e 9f 81 82 b3 79")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 f7 4d ce 39  00 02 26 31 bf c6 4a a2  b8 e1 c9 6a c5 49 a1 47  96 32 56 f1 3f 9b")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 6e a2 3d e1  00 02 3f 54 b9 a0 fc 4a  f4 53 54 5c 8a 19 c8 bb  ad 0c 3c 93 c5 08")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 e7 6a f5 c9  00 02 e9 51 92 d8 e0 b1  52 28 4c 36 c1 3e c2 e6  74 14 29 b3 c9 2f")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 48 7d be ac  00 02 68 39 7c de 6d f3  56 fd fa 9e 87 8e 62 9b  1c 76 8e d4 33 6d")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 31 8d d0 f2  00 02 9d 44 8c 0f 52 77  bc b1 9b dd 40 44 7b 39  0d ae b4 90 b8 02")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 3c 5b 38 81  00 02 e5 3e 84 8b 69 c4  b9 98 e9 52 34 8b b6 8e  22 29 2c 63 c6 c3")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 83 2a 7d d5  00 02 1e 87 ea e6 7d a4  12 1d 1d ae b2 33 e0 85  b4 03 21 37 d4 8f")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 63 74 84 53  00 02 2d 05 7b c0 dc 1f  3a 84 eb 12 6b c8 fb 22  23 29 35 78 87 cf")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 80 dd b1 da  00 02 19 eb 10 1e de ba  e3 75 e5 f5 45 7b 37 dd  9d b2 70 38 9d 2a")]
    [TestCase(DataClassType.SystemPing, "01 01 01 01 eb fa 25 e7  00 02 ec b1 04 53 89 af  40 06 8d ce bd 0a 6b 42  d3 f7 9d 9c 84 b5")]
    [TestCase(DataClassType.SingleResponseAck, "01 01 01 01 82 e8 0b 93  00 04 f8 12 c5 97 11 ef b7  a9 49 47 4f ff 2f 2b  65 d5 29 d5 a2 a2 46 46")]
    [TestCase(DataClassType.SingleResponseAck, "01 01 01 01 ab f7 eb 2d  00 04 f2 9c b8 6b e8 9f  bb 6e 48 41 77 63 ec 04  75 ec da d8 47 2b e1 66")]
    [TestCase(DataClassType.SystemLogin, "01 01 01 01 73 0b a8 fc  00 09 ce f6 10 bc 62 62  50 be 80 b5 61 18 eb 40  13 19 cd b4 85 d5 d9 9f  f5 13 ca b8 27")]
    [TestCase(DataClassType.SingleResponseAck, "01 01 01 01 69 64 2f fd  00 04 96 c6 13 28 0c f8  37 e3 15 d9 6c d7 21 8b  91 ff 3d 82 31 d6 db c5")]
    [TestCase(DataClassType.SingleResponseAck, "01 01 01 01 0c 20 4c ed  00 04 fe c3 1e c3 51 4c  03 c7 22 18 97 e9 44 77  1c c9 a0 ea 81 0a 25 fd")]
    [TestCase(DataClassType.SingleResponseAck, "01 01 01 01 ac 66 eb 74  00 04 96 df a2 2c e3 db  d9 ae ce a5 b5 e4 0e c3  9a a0 54 3a 9f 38 60 a6")]
    [TestCase(DataClassType.SystemConnectionTerminated, "01 01 01 01 4e 18 d0 44  00 00 e6 dc 14 c8 9b 3b  d7 80 40 a2 f2 dd d8 44  93 26 0b bf")]
    [TestCase(DataClassType.SingleResponseAck, "01 01 01 01 3e 23 29 e7  00 01 5b 6e 8f ba 71 b3  19 05 60 93 cc c6 30 9c  2b 5a 1e b7 a7")]
    [TestCase(DataClassType.SystemServerRequest, "01 01 01 01 ab f8 56 11  00 00 65 39 83 b0 59 36 b1 30 c3 7d cb af 19 a5 af e7 4b ea")]
    [TestCase(DataClassType.SingleResponseServerResponse, "01 01 01 01 21 fd 25 91 00 12  58 a9 69 71 d2 f3 cf 6a 29 f0 c6 21 f8 a3 32 70  c2 f0 a3 bc 2b f2 c0 9b fe 25 9b d4 93 35 d6 46  4d e9 d8 b0")]
    [TestCase(DataClassType.MultipleResponseDoor, "01 01 01 01 47 4c ce fe  00 10 4a e4 eb f2 f8 39  d3 6b 1d c7 e3 9a 2a 78  a3 22 46 87 f8 82 30 66  e3 f7 23 30 fc 3c 2d 17  8f 65 1b b6")]
    [TestCase(DataClassType.ApplicationReadAccessLevels, "01 01 01 01 d1 81 c7 9e  00 00 88 85 be d4 0e 4d a0 84 b9 04 4a af 46 87 ef 71 3f 2e")]
    [TestCase(DataClassType.MultipleResponseAccessLevel, "01 01 01 01 48 eb d1 e3  00 0e 8f 35 28 b6 8a 56 e3 0f 1d 26 b7 8e 44 7e b7 75 23 b3 14 04 3a ae  22 20 a5 31 b7 19 eb fc  1d c8")]
    public void DecodeWithTypeTest(DataClassType expectedClassAndType, string data)
    {
        Memory<byte> bytes = Tools.HexStringToBytes(data).ToArray();
        var p = DecodeTest(bytes);

        Assert.That(p.DataClassAndType, Is.EqualTo(expectedClassAndType),
            $"{p.DataClassAndType.String()} vs expected {expectedClassAndType.String()}");
    }

    [TestCase("01 01 01 01 4e 18 d0 44  00 00 e6 dc 14 c8 9b 3b  d7 80 40 a2 f2 dd d8 44  93 26 0b bf")]
    [TestCase("01 01 01 01 73 0b a8 fc  00 09 ce f6 10 bc 62 62  50 be 80 b5 61 18 eb 40  13 19 cd b4 85 d5 d9 9f  f5 13 ca b8 27")]
    [TestCase("01 01 01 01 2f 33 42 9f  00 09 14 ab 18 81 09 2d  ea 43 90 19 3a 87 4b 24  b2 4c 69 67 c5 5b 60 c6  0e d3 16 bc 66")]
    public void PacketRecreationTest(string data)
    {
        Memory<byte> bytes = Tools.HexStringToBytes(data).ToArray();
        Console.Out.WriteHexLine(bytes.Span);
        var p = DecodeTest(bytes);

        if (p.DataItems == null)
            throw new NullReferenceException(nameof(p.DataItems));

        var newP = new BapsiPacket(p.DataClassAndType, p.DataItems.Value.Span);
        Console.WriteLine(newP.ToString());
        Assert.That(newP.Start0, Is.EqualTo(p.Start0), nameof(p.Start0));
        Assert.That(newP.Start1, Is.EqualTo(p.Start1), nameof(p.Start1));
        Assert.That(newP.Version, Is.EqualTo(p.Version), nameof(p.Version));
        Assert.That(newP.Encryption, Is.EqualTo(EncryptionType.Unencrypted), nameof(p.Encryption));
        Assert.That(newP.Length, Is.EqualTo(p.Length), nameof(p.Length));
        Assert.That(newP.DataClassAndType, Is.EqualTo(p.DataClassAndType), $"DataClassAndType {newP.DataClassAndType}");
        Assert.That(newP.DataText, Is.EqualTo(p.DataText), nameof(p.DataText));

        var p2 = DecodeTest(p.GetDecodedPacket(DefaultKey).GetCryptPacket(DefaultKey));
        Assert.That(p2.ToString(), Is.EqualTo(p.ToString()));

        var packetBytes = newP.GetCryptPacket(DefaultKey);
        Console.Out.WriteHexLine(packetBytes.Span);
        var restoreP = DecodeTest(packetBytes);
        Assert.That(restoreP.ToString(), Is.EqualTo(p.ToString()));
    }

    [Test]
    public void VerifyEncodingTest()
    {
        var byteList = new List<byte>(0xff);
        var sb = new StringBuilder(0xff);
        for (byte b = 0x01; ; b++) // byte wraps never ends
        {
            var c = (char)b;
            if (char.IsWhiteSpace(c)
                || char.IsPunctuation(c)
                || char.IsLetterOrDigit(c))
            {
                sb.Append(c);
                byteList.Add(b);
            }
            if (b == 0xff) // end loop
                break;
        }

        var encData = BapsiPacket.DataEncoding.GetString(byteList.ToArray());
        Assert.That(encData, Is.EqualTo(sb.ToString()));
    }
}
