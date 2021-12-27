using System.IO;
using System.Threading;

namespace Imagination.Server.Services
{
    public interface IImageConversionService
    {
        Stream Convert(Stream inputStream, CancellationToken cancelToken);
    }
}