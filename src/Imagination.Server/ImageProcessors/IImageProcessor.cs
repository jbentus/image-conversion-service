using System.IO;
using System.Threading;

namespace Imagination.Server.ImageProcessors
{
    public interface IImageProcessor
    {
        Stream Convert(Stream inputStream, CancellationToken cancelToken);
    }
}