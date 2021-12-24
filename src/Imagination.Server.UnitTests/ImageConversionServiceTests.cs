using Imagination.Server.Services;
using System.IO;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace Imagination.Server.UnitTests;

public class ImageConversionServiceTests
{
    private const string ResxPath = "../../../../../resources";

    [Fact]
    public void TestInvalidInput()
    {
        // Act
        var sut = new ImageConversionService();
        Stream file = sut.Convert(null);

        // Assert
        file.Should().BeNull();
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
        var openForReading = new FileStreamOptions {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Options = FileOptions.Asynchronous | FileOptions.SequentialScan
        };

        await using var inputStream = new FileStream(fileName, openForReading);

        // Act
        Stream outStream = new ImageConversionService().Convert(inputStream);

        // Assert
        outStream.Should().NotBeNull();
        outStream.Should().BeReadable();
    }

    [Fact]
    public async Task TestFailedConversion2JPEG()
    {
        // Arrange
        var openForReading = new FileStreamOptions {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Options = FileOptions.Asynchronous | FileOptions.SequentialScan
        };

        await using var inputStream = new FileStream($"{ResxPath}/invalid_jbento.png", openForReading);

        // Act
        Stream outStream = new ImageConversionService().Convert(inputStream);

        // Assert
        outStream.Should().BeNull();
    }
}