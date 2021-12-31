using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Imagination.Server.ImageProcessors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Imagination.Controllers
{
    [ApiController]
    public class ImageConversionController : ControllerBase
    {
        private readonly ILogger<ImageConversionController> _logger;
        private readonly IImageProcessor _imageProcessor;

        public ImageConversionController(ILogger<ImageConversionController> logger,
            IImageProcessor imageProcessor)
        {
            _logger = logger;
            _imageProcessor = imageProcessor;
        }

        /// <summary>
        /// Converts a bitmap into the JPEG format.
        /// </summary>
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("/convert")]
        public async Task<FileStreamResult> Convert(CancellationToken cancelToken)
        {
            _logger.LogInformation("Received bitmap to convert, length: {ContentLength}", Request.ContentLength);

            Stream jpgStream = await _imageProcessor.ConvertAsync(Request.Body, cancelToken)
                                                    .ConfigureAwait(false);

            return new FileStreamResult(jpgStream, "image/jpeg");
        }
    }
}
