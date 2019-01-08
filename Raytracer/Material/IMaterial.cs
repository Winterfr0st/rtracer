using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Material
{
  public interface IMaterial
  {
    ScatterRecord Scatter(Ray3 rIn, HitRecord record);
  }
}
