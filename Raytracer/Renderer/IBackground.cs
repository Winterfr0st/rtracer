using dyim.RayTracer.Color;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Renderer
{
  public interface IBackground
  {
    IColor GetBackgroundColor(Ray3 r);
  }
}
