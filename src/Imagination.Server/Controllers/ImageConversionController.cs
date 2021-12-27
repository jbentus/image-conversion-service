using System.Threading;
using Imagination.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Imagination.Controllers
{
    [ApiController]
    public class ImageConversionController : ControllerBase
    {
        private readonly ILogger<ImageConversionController> _logger;
        private readonly IImageConversionService _imgConvSvc;

        public ImageConversionController(ILogger<ImageConversionController> logger,
            IImageConversionService imgConvSvc)
        {
            _logger = logger;
            _imgConvSvc = imgConvSvc;
        }

        /// <summary>
        /// Converts a bitmap into the JPEG format.
        /// </summary>
        [HttpPost("/convert")]
        public FileStreamResult Convert(CancellationToken cancelToken)
        {
            _logger.LogInformation($"Received bitmap to convert, length: {Request.ContentLength}");
            
            return new FileStreamResult(_imgConvSvc.Convert(Request.Body, cancelToken),
                                        "image/jpeg");
        }
    }
}
