using System.IO;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System;
using Imagination.Server.Exceptions;
using System.Threading;
using Imagination.Server.ImageProcessors;
using Microsoft.IO;

namespace Imagination.Server.UnitTests;

public class SkiaSharpProcessorTests
{
    private static readonly RecyclableMemoryStreamManager _streamManager = new();

    private const string ResxPath = "../../../../../resources";
    private readonly FileStreamOptions _openForReading = new()
    {
        Mode = FileMode.Open,
        Access = FileAccess.Read,
        Options = FileOptions.Asynchronous | FileOptions.SequentialScan
    };

    [Fact]
    public async Task TestInvalidInput()
    {
        // Arrange
        Exception? ex = null;

        // Act        
        try
        {
            await new SkiaSharpProcessor(_streamManager)
                        .ConvertAsync(null, CancellationToken.None);
        }
        catch(Exception e)
        {
            ex = e;            
        }

        // Assert
        ex.Should().BeOfType<ArgumentNullException>();
        ex.As<ArgumentNullException>().ParamName.Should().Be("inputStream");
    }

    [Theory]
    [InlineData($"{ResxPath}/big.jpg")]
    [InlineData($"{ResxPath}/jfif.jfif")]
    [InlineData($"{ResxPath}/jpeg.jpg")]
    [InlineData($"{ResxPath}/png.png")]
    [InlineData($"{ResxPath}/small.jpg")]
    [InlineData($"{ResxPath}/small.png")]
    [InlineData($"{ResxPath}/transparent.png")]
    [InlineData($"{ResxPath}/invalid.png")]
    public async Task TestConversion2JPEG(string fileName)
    {
        // Arrange
        await using var inputStream = new FileStream(fileName, _openForReading);
        var processor = new SkiaSharpProcessor(_streamManager);

        // Act
        Stream outStream = await processor.ConvertAsync(inputStream, CancellationToken.None);

        // Assert
        outStream.Should().NotBeNull();
        outStream.Should().BeReadable();
    }

    [Fact]
    public async Task TestFailedConversion2JPEG()
    {
        // Arrange
        await using var inputStream = new FileStream($"{ResxPath}/invalid_jbento.png", _openForReading);
        Exception? ex = null;

        // Act        
        try
        {
            await new SkiaSharpProcessor(_streamManager)
                        .ConvertAsync(inputStream, CancellationToken.None);
        }
        catch(Exception e)
        {
            ex = e;            
        }

        // Assert
        ex.Should().BeOfType<ImageConversionFailedException>();
    }
}