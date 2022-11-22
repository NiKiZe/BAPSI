
using BapsiNet.Command;

namespace BapsiNet.Test.Packet;

[TestFixture]
public class IntParserTests
{
    [TestCase(null, "")]
    [TestCase(null, "64 00 00")]
    [TestCase(null, "00 00")]
    [TestCase(100, "31 30 30 00")]
    [TestCase(400, "34 30 30")]
    [TestCase(0, "30")]
    [TestCase(4, "30 34")]
    [TestCase(9, "39 00")]
    [TestCase(null, "31 30 65 00")]
    [TestCase(null, "31 30 30 00 31")]
    public void BaseParseIntTest(int? expected, string data)
    {
        Memory<byte> bytes = Tools.HexStringToBytes(data).ToArray();
        Assert.That(BapsiCommand.GetInteger(bytes.Span), Is.EqualTo(expected));
    }
}
