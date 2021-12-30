using System.Buffers;
using SkiaSharp;

public class SkiaSharpProcessor
{

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

}
