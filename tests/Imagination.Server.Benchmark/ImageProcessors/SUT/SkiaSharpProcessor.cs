using System.Buffers;
using Microsoft.IO;
using SkiaSharp;

public class SkiaSharpProcessor
{
    private const string FailedToDecodeMsg = "Failed to decode the bitmap";
    private const string FailedToEncodeMsg = "Failed to encode the bitmap to JPEG format";

    private static readonly RecyclableMemoryStreamManager _streamManager = new();

    public Stream Convert(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        SKBitmap bitmap = SKBitmap.Decode(inputStream);

        if (bitmap == null)
        {
            throw new ImageConversionFailedException(FailedToDecodeMsg);
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException(FailedToDecodeMsg);
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
            throw new ImageConversionFailedException(FailedToDecodeMsg);
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException(FailedToDecodeMsg);
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
            throw new ImageConversionFailedException(FailedToDecodeMsg);
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException(FailedToDecodeMsg);
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }

    public async Task<Stream> ConvertAsyncRecycableMemoryStream(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        SKBitmap bitmap;

        try
        {
            using var memStream = _streamManager.GetStream();
            await inputStream.CopyToAsync(memStream, cancelToken);

            // To force the Stream Writer to flush.
            memStream.Position = 0;

            bitmap = SKBitmap.Decode(memStream);
        }
        catch (ArgumentNullException)
        {
            throw new ImageConversionFailedException(FailedToDecodeMsg);
        }

        if (bitmap == null)
        {
            throw new ImageConversionFailedException(FailedToDecodeMsg);
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = _streamManager.GetStream();

        if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
        {
            throw new ImageConversionFailedException(FailedToEncodeMsg);
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }
}
