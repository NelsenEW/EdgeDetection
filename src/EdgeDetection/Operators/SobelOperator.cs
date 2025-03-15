using System;
using System.Drawing;
using EdgeDetection.Services;

namespace EdgeDetection.Operators
{
    public class SobelOperator : IEdgeOperator
    {
        public Bitmap Apply(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap result = new Bitmap(width, height);

            int[,] gx = new int[,]
            {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };

            int[,] gy = new int[,]
            {
                { 1, 2, 1 },
                { 0, 0, 0 },
                { -1, -2, -1 }
            };

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int pixelX = 0;
                    int pixelY = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color pixel = image.GetPixel(x + j, y + i);
                            int grayValue = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                            pixelX += grayValue * gx[i + 1, j + 1];
                            pixelY += grayValue * gy[i + 1, j + 1];
                        }
                    }

                    int magnitude = (int)Math.Sqrt(pixelX * pixelX + pixelY * pixelY);
                    magnitude = Math.Min(255, Math.Max(0, magnitude));

                    result.SetPixel(x, y, Color.FromArgb(magnitude, magnitude, magnitude));
                }
            }

            return result;
        }
    }
}