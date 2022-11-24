
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Implements <see cref="DataClassType.SingleResponseServerResponse"/></summary>
public class SingleResponseServerResponseCommand : ParameterizedCommand
{
    public override DataClassType DataClassAndType => DataClassType.SingleResponseServerResponse;
    public ServerTypeType ServerType => (ServerTypeType)Convert.ToInt32(Parameters[0]);
    public string Version => Parameters[1];
    public int Revision => Convert.ToInt32(Parameters[2]);

    private SingleResponseServerResponseCommand() : base(Array.Empty<string>()) { }

    public enum ServerTypeType
    {
        Entro = 1,
    }

    public SingleResponseServerResponseCommand(ServerTypeType serverType, string version, int revision)
        : base(new string[] { ((int)serverType).ToString(), version, revision.ToString()}) {}

    public SingleResponseServerResponseCommand(ReadOnlyMemory<byte> data)
        : base(data) {}

    public override string ToString() => $"SingleResponseServerResponseCommand : Type: {ServerType} Version: {Version} Revision: {Revision}";
}

