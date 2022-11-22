
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Base class for tab 0x09 separated data</summary>
public abstract class ParameterizedCommand : BapsiCommand
{
    public string[] Parameters { get; private set; }
    internal override ReadOnlyMemory<byte> Data => GetBytes(string.Join('\t', Parameters));

    protected ParameterizedCommand(string[] parameters)
        : base()
    {
        Parameters = parameters;
    }

    public ParameterizedCommand(ReadOnlyMemory<byte> data)
        : this(GetString(data).Split('\t'))
    {
    }

    public override string ToString() => $"Parameters : {string.Join('|', Parameters)}";
}

