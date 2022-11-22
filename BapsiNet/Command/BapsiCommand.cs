
using BapsiNet.Packet;
using System.Reflection;

namespace BapsiNet.Command;

public abstract class BapsiCommand
{
    public abstract DataClassType DataClassAndType { get; }

    internal abstract ReadOnlyMemory<byte> Data { get; }

    public virtual BapsiPacket GetPacket() => new(DataClassAndType, Data.Span);

    protected static ReadOnlyMemory<byte> GetBytes(string str) => BapsiPacket.DataEncoding.GetBytes(str);
    protected static string GetString(ReadOnlyMemory<byte> data) => BapsiPacket.DataEncoding.GetString(data.Span);

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

    protected static int GetIntFromData(ReadOnlyMemory<byte> data) =>
        GetInteger(data.Span)
        ?? throw new ArgumentOutOfRangeException(nameof(data), data, "was not valid integer string data");

    private static readonly Dictionary<DataClassType, Func<ReadOnlyMemory<byte>, BapsiCommand>> LookupDataClassAndType
        = GetCommandCreatorTypes().ToDictionary(GetDataClassAndTypeForType, GetCtorFunc);

    #region Reflection handling
    private static Type[] GetClassInheritedFrom<T>() => typeof(T).Assembly
        .GetTypes().Where(t => !t.IsAbstract && typeof(T).IsAssignableFrom(t)).ToArray();

    private static ConstructorInfo? GetCommandCtorInfo(Type t) => t.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public,
            new Type[] { typeof(ReadOnlyMemory<byte>) });

    private static Type[] GetCommandCreatorTypes() =>
        GetClassInheritedFrom<BapsiCommand>()
        .Where(t => GetCommandCtorInfo(t) != null)
        .ToArray();

    private static DataClassType GetDataClassAndTypeForType(Type t) => 
        ((BapsiCommand?)Activator.CreateInstance(t, true))?
        .DataClassAndType ??
        throw new NotImplementedException($"{t} is likely missing private empty constructor");

    private static Func<ReadOnlyMemory<byte>, BapsiCommand> GetCtorFunc(Type t) => 
        (ReadOnlyMemory<byte> data) => (BapsiCommand)GetCommandCtorInfo(t)!
        .Invoke(new object[] { data });
    #endregion

    public static BapsiCommand GetInstance(DataClassType dct, ReadOnlyMemory<byte> data)
    {
        if (LookupDataClassAndType.TryGetValue(dct, out var ctorFunc))
            return ctorFunc(data);

        if (data.Length == 0)
            return new GenericCommand(dct);

        System.Diagnostics.Debug.WriteLine($"Unknown {dct.String()} type (no specific handler) with data: {BapsiPacket.DebugByteData(data.Span)}");

        // Data with tabs is parameterized
        if (data.Span.Contains((byte)'\t'))
            return new GenericParameterizedCommand(dct, data);

        return new GenericDataCommand(dct, data);
    }
}
