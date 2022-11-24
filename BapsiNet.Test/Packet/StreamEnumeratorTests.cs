
using BapsiNet.Packet;

namespace BapsiNet.Test.Packet;

[TestFixture]
public class StreamEnumeratorTests
{
    [TestCase(1, "01 01 01 01 63 48 50 4e  00 02 54 de 1c ef 78 93 1a 92 bc 41 de 61 33 a4  ac 93 80 53 6c b5")]
    [TestCase(2, "01 01 01 01 52 96 df e6  00 02 e1 fa 4b f8 d7 9c 2f d7 68 1d 87 19 b0 66  de a8 92 8a 90 f0 01 01 01 01 21 fd 25 91 00 12  58 a9 69 71 d2 f3 cf 6a 29 f0 c6 21 f8 a3 32 70  c2 f0 a3 bc 2b f2 c0 9b fe 25 9b d4 93 35 d6 46  4d e9 d8 b0")]
    [TestCase(1, "01 01 01 01 d9 f3 c6 54  00 01 21 76 28 91 94 88 76 b2 75 c3 44 21 70 b4  1c 12 83 cd 8c")]
    public async Task EnumeratePackagesTest(int expectedPackets, string data)
    {
        using var ms = new MemoryStream(Tools.HexStringToBytes(data).ToArray());
        var i = 0;
        await foreach (var (p, buf) in StreamEnumerator.ReadPacketAsync(ms, CancellationToken.None))
        {
            PacketTests.DecodeTest(p!, buf);
            i++;
        }
        Assert.That(i, Is.EqualTo(expectedPackets));
    }

}
