using System.Text;

namespace BapsiNet.Test;

public static class Tools
{
    public static bool IsHex(this char c)
    {
        return (c >= '0' && c <= '9') ||
                (c >= 'a' && c <= 'f') ||
                (c >= 'A' && c <= 'F');
    }

    public static IEnumerable<byte> HexStringToBytes(string str)
    {
        var sb = new StringBuilder(2); // must be a more efficient way than this?
        // not handling 0xXX sets
        foreach (var c in str)
        {
            if (c == ' ' && sb.Length != 0)
                throw new IndexOutOfRangeException(sb.ToString() + " expected empty");
            if (!c.IsHex())
                continue;

            sb.Append(c);
            if (sb.Length == 2)
            {
                yield return Convert.ToByte(sb.ToString(), 16);
                sb.Clear();
            }
        }
    }
}
