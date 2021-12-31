using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imagination.Server.ImageProcessors
{
    public interface IImageProcessor
    {
        Task<Stream> ConvertAsync(Stream inputStream, CancellationToken cancelToken);
    }
}