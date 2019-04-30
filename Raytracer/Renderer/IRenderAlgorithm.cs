using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Renderer
{
  interface IRenderAlgorithm
  {
    ILightPath Render(Ray3 r, HitableList world);
  }
}
