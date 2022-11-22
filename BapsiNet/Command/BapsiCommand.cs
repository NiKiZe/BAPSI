
using BapsiNet.Packet;

namespace BapsiNet.Command;

public abstract class BapsiCommand
{
    public abstract DataClassType DataClassAndType { get; }

    internal abstract ReadOnlyMemory<byte> Data { get; }

    public virtual BapsiPacket GetPacket() => new(DataClassAndType, Data.Span);

    #region GetInteger
    private static int GetCharInt(char c) => '0' <= c && c <= '9' ? c - '0' : -1;

    public static int? GetInteger(ReadOnlySpan<byte> data)
    {
        int i = 0;
        bool hasDigit = false;
        bool hasNull = false;
        foreach (var b in data)
        {
            if (b == 0)
            {
                if (!hasDigit)
                    return null;
                hasNull = true;
                continue;
            }

            var cval = GetCharInt((char)b);
            if (cval == -1)
                return null;

            // we got digit after null, so invalid
            if (hasNull)
                return null;

            i *= 10;
            i += cval;
            hasDigit = true;
        }
        if (!hasDigit)
            return null;
        return i;
    }
    #endregion
}
