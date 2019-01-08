using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Shapes
{
  public interface IHitable
  {
    HitRecord Hit(Ray3 r, double tMin, double tMax);
  }
}
