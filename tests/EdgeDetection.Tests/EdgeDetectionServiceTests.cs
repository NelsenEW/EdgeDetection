using System;
using System.Collections.Generic; // Add this line
using System.Drawing;
using Xunit;
using EdgeDetection.Services;
using EdgeDetection.Operators;

namespace EdgeDetection.Tests
{
    public class EdgeDetectionServiceTests
    {
        private IEdgeDetectionService _edgeDetectionService;

        public EdgeDetectionServiceTests()
        {
            var operators = new Dictionary<string, IEdgeOperator>
            {
                { "Sobel", new SobelOperator() },
                { "Prewitt", new PrewittOperator() }
            };
            _edgeDetectionService = new EdgeDetectionService(operators);
        }

        [Fact]
        public void ApplySobelOperator_ShouldReturnEdgeDetectedImage()
        {
            // Arrange
            var imageData = new Bitmap(2, 2);
            imageData.SetPixel(0, 0, Color.Black);
            imageData.SetPixel(0, 1, Color.White);
            imageData.SetPixel(1, 0, Color.White);
            imageData.SetPixel(1, 1, Color.Black);

            // Act
            var result = _edgeDetectionService.ApplyEdgeDetection(imageData, "Sobel");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Width);
            Assert.Equal(2, result.Height);
        }

        [Fact]
        public void ApplyPrewittOperator_ShouldReturnEdgeDetectedImage()
        {
            // Arrange
            var imageData = new Bitmap(2, 2);
            imageData.SetPixel(0, 0, Color.Black);
            imageData.SetPixel(0, 1, Color.White);
            imageData.SetPixel(1, 0, Color.White);
            imageData.SetPixel(1, 1, Color.Black);

            // Act
            var result = _edgeDetectionService.ApplyEdgeDetection(imageData, "Prewitt");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Width);
            Assert.Equal(2, result.Height);
        }

        [Fact]
        public void ApplyEdgeDetection_WithInvalidOperator_ShouldThrowArgumentException()
        {
            // Arrange
            var imageData = new Bitmap(2, 2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _edgeDetectionService.ApplyEdgeDetection(imageData, "InvalidOperator"));
        }

        [Fact]
        public void ApplyEdgeDetection_WithNullImage_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _edgeDetectionService.ApplyEdgeDetection(null, "Sobel"));
        }
    }
}