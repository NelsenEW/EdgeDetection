using System.Drawing;

namespace EdgeDetection.Services
{
    public interface IEdgeDetectionService
    {
        Bitmap ApplyEdgeDetection(Bitmap image, string operatorType);
    }
}