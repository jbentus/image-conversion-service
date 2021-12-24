using System;

namespace Imagination.Server.Exceptions
{
    public class ImageConversionFailedException : Exception
    {
        public ImageConversionFailedException(string msg) : base(msg) {}
    }
}