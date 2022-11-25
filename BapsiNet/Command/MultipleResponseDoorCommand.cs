
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Implements <see cref="DataClassType.MultipleResponseDoor"/></summary>
public class MultipleResponseDoorCommand : ParameterizedCommand
{
    public override DataClassType DataClassAndType => DataClassType.MultipleResponseDoor;

    public string Name => Parameters[0];
    public int Address => Convert.ToInt32(Parameters[1]);
    public int Controller => Convert.ToInt32(Parameters[2]);
    public string Type => Parameters[3];
    public int Index => Convert.ToInt32(Parameters[4]);

    private MultipleResponseDoorCommand() : base(Array.Empty<string>()) { }

    public MultipleResponseDoorCommand(ReadOnlyMemory<byte> data)
        : base(data) { }

    public override string ToString() => $"MultipleResponseDoorCommand : Name: {Name} Address: {Address} Ctrl: {Controller} Type: {Type} Idx: {Index}";
}
