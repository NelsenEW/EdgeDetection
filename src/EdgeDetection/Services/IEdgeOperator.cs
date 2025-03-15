using System.Drawing;

namespace EdgeDetection.Services
{
    public interface IEdgeOperator
    {
        Bitmap Apply(Bitmap image);
    }
}
