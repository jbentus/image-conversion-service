using System.IO;
using SkiaSharp;

namespace Imagination.Server.Services
{
    public class ImageConversionService
    {
        public ImageConversionService()
        {
        }

        public Stream Convert(Stream inputStream)
        {
            if (inputStream == null)
            {
                // TODO: log error
                return null;
            }

            SKBitmap bitmap = SKBitmap.Decode(inputStream);

            if (bitmap == null)
            {
                // TODO: log error
                return null;
            }

            var outStream = new MemoryStream();

            if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
            {
                // TODO: log error
                return null;
            }

            return outStream;
        }
    }
}