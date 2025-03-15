using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace EdgeDetection.Services
{
    public class EdgeDetectionService : IEdgeDetectionService
    {
        private readonly Dictionary<string, IEdgeOperator> _operators;

        public EdgeDetectionService(Dictionary<string, IEdgeOperator> operators)
        {
            _operators = operators;
        }

        public Bitmap ApplyEdgeDetection(Bitmap inputImage, string operatorType)
        {
            if (inputImage == null)
            {
                throw new ArgumentNullException(nameof(inputImage), "Input image cannot be null.");
            }

            if (_operators.TryGetValue(operatorType, out var edgeOperator))
            {
                Bitmap grayImage = ConvertToGrayscale(inputImage);
                Bitmap edgeDetectedImage = edgeOperator.Apply(grayImage);

                return edgeDetectedImage;
            }
            throw new ArgumentException($"Operator {operatorType} not found.");
        }

        private Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap grayScaleImage = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color originalColor = original.GetPixel(x, y);
                    int grayValue = (int)(originalColor.R * 0.299 + originalColor.G * 0.587 + originalColor.B * 0.114);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayScaleImage.SetPixel(x, y, grayColor);
                }
            }

            return grayScaleImage;
        }
    }
}