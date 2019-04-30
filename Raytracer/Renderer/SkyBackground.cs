using dyim.RayTracer.Color;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Renderer
{
  public class SkyBackground : IBackground
  {
    private static readonly IColor SkyColor = new RGBColor(0.5, 0.7, 1.0);

    public IColor GetBackgroundColor(Ray3 r)
    {
      Vector3 unitDir = r.Direction.ToUnitVector();
      double t = 0.5 * (unitDir.Y + 1.0);
      IColor skylight = RGBColor.White.Multiply(1.0 - t).Add(SkyColor.Multiply(t));
      return skylight;
    }
  }
}
