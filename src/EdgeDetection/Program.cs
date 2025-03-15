using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using EdgeDetection.Services;
using EdgeDetection.Operators;

namespace EdgeDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: EdgeDetection <imagePath> [operator|Sobel] [outputPath|imagePath-operator.bmp]");
                return;
            }

            string imagePath = args[0];
            string operatorChoice = args.Length > 1 ? args[1] : "Sobel";
            string outputImagePath = args.Length > 2 ? args[2] : Path.Combine(Path.GetDirectoryName(imagePath), $"{Path.GetFileNameWithoutExtension(imagePath)}-{operatorChoice}.bmp");

            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Error: File not found at {imagePath}");
                return;
            }

            Bitmap inputImage;
            try
            {
                inputImage = new Bitmap(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to load image. {ex.Message}");
                return;
            }

            var operators = new Dictionary<string, IEdgeOperator>
            {
                { "Sobel", new SobelOperator() },
                { "Prewitt", new PrewittOperator() }
            };

            var edgeDetectionService = new EdgeDetectionService(operators);

            try
            {
                Bitmap resultImage = edgeDetectionService.ApplyEdgeDetection(inputImage, operatorChoice);
                resultImage.Save(outputImagePath, System.Drawing.Imaging.ImageFormat.Bmp);
                Console.WriteLine($"Edge detection completed. Output saved at {outputImagePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}