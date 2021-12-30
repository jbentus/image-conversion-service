using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

public class ImageSharpProcessor
{
    public Stream Convert(Stream inputStream, CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(inputStream);

        Image bitmap;
        try
        {
            bitmap = Image.Load(inputStream);
        }
        catch (Exception ex)
        {
            throw new ImageConversionFailedException(ex.Message);
        }

        cancelToken.ThrowIfCancellationRequested();

        var outStream = new MemoryStream();

        try
        {
            bitmap.SaveAsJpeg(outStream, new JpegEncoder() { Quality = 80 });
        }
        catch (Exception ex)
        {
            throw new ImageConversionFailedException(ex.Message);
        }

        // To force the Stream Writer to flush.
        outStream.Position = 0;

        return outStream;
    }
}
