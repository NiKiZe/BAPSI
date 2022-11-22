
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Implements <see cref="DataClassType.SystemPing"/></summary>
public class PingRequestCommand : BapsiCommand
{
    public override DataClassType DataClassAndType => DataClassType.SystemPing;
    public int Seconds { get; private set; }
    internal override ReadOnlyMemory<byte> Data => GetBytes(Seconds.ToString());

    private PingRequestCommand() : base() { }

    public PingRequestCommand(int seconds)
        : base()
    {
        Seconds = seconds;
    }

    public PingRequestCommand(ReadOnlyMemory<byte> data)
        : this(GetIntFromData(data))
    {
    }

    public override string ToString() => $"PingRequest Seconds: {Seconds}";
}
