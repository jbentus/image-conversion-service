using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Imagination.Server.Exceptions;
using Microsoft.IO;
using SkiaSharp;

namespace Imagination.Server.ImageProcessors
{
    public class SkiaSharpProcessor : IImageProcessor
    {
        private const string FailedToDecodeMsg = "Failed to decode the bitmap";
        private const string FailedToEncodeMsg = "Failed to encode the bitmap to JPEG format";

        private readonly RecyclableMemoryStreamManager _streamManager;

        public SkiaSharpProcessor(RecyclableMemoryStreamManager streamManager)
        {
            _streamManager = streamManager;
        }

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
        public async Task<Stream> ConvertAsync(Stream inputStream, CancellationToken cancelToken)
        {
            ArgumentNullException.ThrowIfNull(inputStream);

            SKBitmap bitmap;
            
            try
            {
                await using var memStream = _streamManager.GetStream();
                await inputStream.CopyToAsync(memStream, cancelToken);
                
                // To force the Stream Writer to flush.
                memStream.Position = 0;

                bitmap = SKBitmap.Decode(memStream);
            }
            catch(ArgumentNullException)
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
}