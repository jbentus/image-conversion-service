using Imagination.Server.Exceptions;
using Imagination.Server.ImageProcessors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

public class ImageSharpProcessor : IImageProcessor
{
    /// <summary>
    /// Convert a bitmap from a given Stream to the JPEG format.
    /// </summary>
    /// <returns> The converted bitmap in JPEG. </returns>
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when <paramref name="inputStream"/> is null.
    /// </exception>
    /// <exception cref="Imagination.Server.Exceptions.ImageConversionFailedException">
    /// Thrown when the input stream is invalid.
    /// </exception>
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
