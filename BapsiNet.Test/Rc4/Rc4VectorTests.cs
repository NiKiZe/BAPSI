
namespace BapsiNet.Test.Rc4;

[TestFixture]
public class Rc4VectorTests
{
    // https://datatracker.ietf.org/doc/html/rfc6229
    [TestCase("01 02 03 04 05", "00 00 00 00  00 00 00 00   00 00 00 00  00 00 00 00", "b2 39 63 05  f0 3d c0 27   cc c3 52 4a  0a 11 18 a8")]
    [TestCase("01 02 03 04 05", "b2 39 63 05  f0 3d c0 27   cc c3 52 4a  0a 11 18 a8", "00 00 00 00  00 00 00 00   00 00 00 00  00 00 00 00")]
    [TestCase("0102030405060708090a0b0c0d0e0f101112131415161718", "00 00 00 00  00 00 00 00   00 00 00 00  00 00 00 00", "05 95 e5 7f  e5 f0 bb 3c   70 6e da c8  a4 b2 db 11")]
    [TestCase("0102030405060708090a0b0c0d0e0f101112131415161718", "05 95 e5 7f  e5 f0 bb 3c   70 6e da c8  a4 b2 db 11", "00 00 00 00  00 00 00 00   00 00 00 00  00 00 00 00")]
    [TestCase("99 99 99 99 99 99 99 99 99 99 99 99 99 99 99 99 00 00 00 00", "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", "04 93 c1 0e 93 c7 ef 86 61 0a 9d 04 29 a8 23 c5 0e 0e 28")]
    public void RunVectors(string hexKey, string hexData, string hexResult)
    {
        var key = Tools.HexStringToBytes(hexKey).ToArray();
        var data = Tools.HexStringToBytes(hexData).ToArray();
        var expectedResult = Tools.HexStringToBytes(hexResult).ToArray();
        Assert.That(expectedResult.Length, Is.EqualTo(data.Length));
        var rc4 = new RC4(key);
        rc4.Apply(data);

        Console.Out.WriteHexLine(data);
        Assert.That(data, Is.EqualTo(expectedResult));
    }
}
