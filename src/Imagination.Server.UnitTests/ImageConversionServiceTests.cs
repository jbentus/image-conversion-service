using Imagination.Server.Services;
using System.IO;
using Xunit;
using FluentAssertions;

namespace Imagination.Server.UnitTests;

public class ImageConversionServiceTests
{
    [Fact]
    public void TestInvalidInput()
    {
        // Act
        var sut = new ImageConversionService();
        Stream file = sut.Convert(null);

        // Assert
        file.Should().BeNull();
    }
}