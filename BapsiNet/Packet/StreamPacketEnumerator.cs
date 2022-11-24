
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace BapsiNet.Packet;

public static class StreamEnumerator
{
    public static async IAsyncEnumerable<(BapsiPacket?, Memory<byte>)>
        ReadPacketAsync(TcpClient client, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await Task.Yield();
        var readStream = client.GetStream();
        while (!cancellationToken.IsCancellationRequested &&
            client.Connected)
        {
            await foreach (var x in ReadPacketAsync(readStream, cancellationToken))
                yield return x;
        }
        client.Close();
        readStream?.Dispose();
    }

    public static async IAsyncEnumerable<(BapsiPacket?, Memory<byte>)>
        ReadPacketAsync(Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const ushort HeaderSize = 28;
        Memory<byte> buffer = new byte[1024];
        int read;
        while ((read = await stream.ReadAsync(buffer[..HeaderSize], cancellationToken).ConfigureAwait(false)) != 0)
        {
            if (read < 28
                || buffer.Span[0] != 0x01 // SOH 0
                || buffer.Span[1] != 0x01 // SOH 1
                || buffer.Span[2] != 1 /* Version */)
            {
                yield return (null, buffer[..read].ToArray());
            }
            else
            {
                var length = HeaderSize + (ushort)((buffer.Span[8] << 8) + buffer.Span[9]);
                var start = read;
                while (start < length &&
                    (read = await stream.ReadAsync(buffer[start..length], cancellationToken).ConfigureAwait(false)) != 0)
                {
                    start += read;
                }
                var buf = buffer[..length].ToArray();
                yield return (new BapsiPacket(buf), buf);
            }
        }
    }
}
