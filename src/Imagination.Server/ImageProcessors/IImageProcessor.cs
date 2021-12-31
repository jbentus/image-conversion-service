using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imagination.Server.ImageProcessors
{
    public interface IImageProcessor
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
        Task<Stream> ConvertAsync(Stream inputStream, CancellationToken cancelToken);
    }
}