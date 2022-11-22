
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Generic command with data</summary>
public class GenericDataCommand : BapsiCommand
{
    private readonly DataClassType _dct;
    public override DataClassType DataClassAndType => _dct;
    private readonly ReadOnlyMemory<byte> _data;
    internal override ReadOnlyMemory<byte> Data => _data;

    public GenericDataCommand(DataClassType dct, ReadOnlyMemory<byte> data)
        : base()
    {
        _dct = dct;
        _data = data;
    }

    public override string ToString() => $"GenericDataCommand: {_dct.String()} D: {BapsiPacket.DebugByteData(_data.Span)}";
}
