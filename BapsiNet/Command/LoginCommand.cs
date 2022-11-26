
using BapsiNet.Packet;

namespace BapsiNet.Command;

/// <summary>Implements <see cref="DataClassType.SystemLogin"/></summary>
public class LoginCommand : BapsiCommand
{
    public override DataClassType DataClassAndType => DataClassType.SystemLogin;
    public string BapsiUser { get; private set; }
    internal override ReadOnlyMemory<byte> Data => GetBytes(BapsiUser);

    private LoginCommand() : this(string.Empty) { }

    public LoginCommand(string bapsiUser)
        : base()
    {
        BapsiUser = bapsiUser;
    }

    public LoginCommand(ReadOnlyMemory<byte> data)
        : this(GetString(data))
    {
    }

    public override string ToString() => $"LoginCommand BapsiUser: {BapsiUser}";
}
