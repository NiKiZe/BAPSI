
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Implements <see cref="DataClassType.SingleResponseAck"/></summary>
public class SingleResponseAckCommand : BapsiCommand
{
    public override DataClassType DataClassAndType => DataClassType.SingleResponseAck;
    public ResponseAckType AckType { get; private set; }
    internal override ReadOnlyMemory<byte> Data => GetBytes(((int)AckType).ToString());

    private SingleResponseAckCommand() : base() { }

    public SingleResponseAckCommand(ResponseAckType ackType)
        : base()
    {
        AckType = ackType;
    }

    public SingleResponseAckCommand(ReadOnlyMemory<byte> data)
        : this((ResponseAckType)GetIntFromData(data))
    {
    }

    public override string ToString() => $"SingleResponseAckCommand: {AckType}";
}
