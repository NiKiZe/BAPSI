
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Generic command for parameterized data</summary>
public class GenericParameterizedCommand : ParameterizedCommand
{
    private readonly DataClassType _dct;
    public override DataClassType DataClassAndType => _dct;

    public GenericParameterizedCommand(DataClassType dct, ReadOnlyMemory<byte> data)
        : base(data)
    {
        _dct = dct;
    }

    public override string ToString() => $"GenericParameterizedCommand: {_dct.String()} {base.ToString()} D: {BapsiPacket.DebugByteData(Data.Span)}";
}
