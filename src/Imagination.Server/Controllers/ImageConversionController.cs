using System.Threading;
using Imagination.Server.ImageProcessors;
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
        [HttpPost("/convert")]
        public FileStreamResult Convert(CancellationToken cancelToken)
        {
            _logger.LogInformation($"Received bitmap to convert, length: {Request.ContentLength}");
            
            return new FileStreamResult(_imageProcessor.Convert(Request.Body, cancelToken),
                                        "image/jpeg");
        }
    }
}
