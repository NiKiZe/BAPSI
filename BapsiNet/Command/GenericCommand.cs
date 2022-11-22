
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Generic command with no data</summary>
public class GenericCommand : BapsiCommand
{
    private readonly DataClassType _dct;
    public override DataClassType DataClassAndType => _dct;

    public GenericCommand(DataClassType dct)
        : base()
    {
        _dct = dct;
    }

    internal override ReadOnlyMemory<byte> Data => Array.Empty<byte>();

    public override string ToString() => $"GenericCommand: {_dct.String()}";
}
