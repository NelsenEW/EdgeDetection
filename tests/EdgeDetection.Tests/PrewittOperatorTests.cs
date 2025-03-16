using System;
using System.Drawing;
using Xunit;
using EdgeDetection.Operators;

namespace EdgeDetection.Tests
{
    public class PrewittOperatorTests
    {
        [Fact]
        public void Apply_ShouldReturnEdgeDetectedImage()
        {
            // Arrange
            var prewittOperator = new PrewittOperator();
            var imageData = new Bitmap(2, 2);
            imageData.SetPixel(0, 0, Color.Black);
            imageData.SetPixel(0, 1, Color.White);
            imageData.SetPixel(1, 0, Color.White);
            imageData.SetPixel(1, 1, Color.Black);

            // Act
            var result = prewittOperator.Apply(imageData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Width);
            Assert.Equal(2, result.Height);
        }
    }
}
