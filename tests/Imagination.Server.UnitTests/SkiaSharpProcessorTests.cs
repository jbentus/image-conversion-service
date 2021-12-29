using System.IO;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System;
using Imagination.Server.Exceptions;
using System.Threading;
using Imagination.Server.ImageProcessors;

namespace Imagination.Server.UnitTests;

public class SkiaSharpProcessorTests
{
    private const string ResxPath = "../../../../../resources";
    private readonly FileStreamOptions _openForReading = new()
    {
        Mode = FileMode.Open,
        Access = FileAccess.Read,
        Options = FileOptions.Asynchronous | FileOptions.SequentialScan
    };

    [Fact]
    public void TestInvalidInput()
    {
        // Arrange
        Exception? ex = null;

        // Act        
        try
        {
            new SkiaSharpProcessor()
                    .Convert(null, CancellationToken.None);
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

        // Act
        Stream outStream = new SkiaSharpProcessor()
                                .Convert(inputStream, CancellationToken.None);

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
            new SkiaSharpProcessor()
                    .Convert(inputStream, CancellationToken.None);
        }
        catch(Exception e)
        {
            ex = e;            
        }

        // Assert
        ex.Should().BeOfType<ImageConversionFailedException>();
    }
}