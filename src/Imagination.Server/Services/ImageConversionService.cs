﻿using System;
using System.IO;
using Imagination.Server.Exceptions;
using SkiaSharp;

namespace Imagination.Server.Services
{
    public class ImageConversionService
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
        public Stream Convert(Stream inputStream)
        {
            ArgumentNullException.ThrowIfNull(inputStream);

            SKBitmap bitmap = SKBitmap.Decode(inputStream);

            if (bitmap == null)
            {
                throw new ImageConversionFailedException("Failed to decode the bitmap using the specified stream");
            }

            var outStream = new MemoryStream();

            if (!bitmap.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
            {
                throw new ImageConversionFailedException("Failed to encode the bitmap to JPEG format");
            }

            return outStream;
        }
    }
}