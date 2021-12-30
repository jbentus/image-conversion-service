using System.Buffers;
using Microsoft.IO;
using SkiaSharp;

public class SkiaSharpProcessor
{
    private static readonly RecyclableMemoryStreamManager _streamManager = new();

    public Stream Convert(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        SKBitmap bitmap = SKBitmap.Decode(inputStream);

        if (bitmap == null)
        {
            throw new ImageConversionFailedException("Failed to decode the bitmap using the specified stream");
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException("Failed to encode the bitmap to JPEG format");
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }

    public async Task<Stream> ConvertAsync(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        var buffer = new Memory<byte>(new byte[inputStream.Length]);
        await inputStream.ReadAsync(buffer, cancelToken);

        cancelToken.ThrowIfCancellationRequested();

        SKBitmap bitmap = SKBitmap.Decode(buffer.Span);

        if (bitmap == null)
        {
            throw new ImageConversionFailedException("Failed to decode the bitmap using the specified stream");
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException("Failed to encode the bitmap to JPEG format");
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }

    public async Task<Stream> ConvertAsyncMemoryPool(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        byte[] buffer = ArrayPool<byte>.Shared.Rent((int)inputStream.Length);
        await inputStream.ReadAsync(buffer, cancelToken);

        cancelToken.ThrowIfCancellationRequested();

        SKBitmap bitmap = SKBitmap.Decode(buffer);

        ArrayPool<byte>.Shared.Return(buffer);

        if (bitmap == null)
        {
            throw new ImageConversionFailedException("Failed to decode the bitmap using the specified stream");
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException("Failed to encode the bitmap to JPEG format");
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }
        
    public async Task<Stream> ConvertAsyncMemAndStreamPooled(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        byte[] buffer = ArrayPool<byte>.Shared.Rent((int)inputStream.Length);
        await inputStream.ReadAsync(buffer, cancelToken);

        cancelToken.ThrowIfCancellationRequested();

        SKBitmap bitmap = SKBitmap.Decode(buffer);

        ArrayPool<byte>.Shared.Return(buffer);

        if (bitmap == null)
        {
            throw new ImageConversionFailedException("Failed to decode the bitmap using the specified stream");
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = _streamManager.GetStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException("Failed to encode the bitmap to JPEG format");
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }
}
