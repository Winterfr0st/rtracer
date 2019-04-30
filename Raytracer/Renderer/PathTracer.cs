using System;
using dyim.RayTracer.Color;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Renderer
{
  public class PathTracer : IRenderAlgorithm
  {
    private readonly int maxDepth;
    private readonly IBackground background;

    public PathTracer(IBackground background, int maxDepth)
    {
      this.maxDepth = maxDepth;
      this.background = background;
    }

    public ILightPath Render(Ray3 r, HitableList world)
    {
      return this.Color(r, world, 0);
    }

    private ILightPath Color(Ray3 r, HitableList world, int depth)
    {
      HitRecord record = world.Hit(r, 0.01, 10000);
      if (null != record)
      {
        if (depth < this.maxDepth)
        {
          ScatterRecord sr = record.Material.Scatter(r, record);
          if (null != sr)
          {
            ILightPath next = Color(sr.Scatter, world, depth + 1);
            var node = new PathtracerLightPath(sr.Attenuation, next);
            return node;
          }
        }

        // Either got absorbed by material or this ray refracted/reflected 50 times.
        // Don't want to go more than 50 depth so stop here in that case.
        return new PathtracerLightPath(RGBColor.Black, null);
      }

      // Ray hits nothing. Return background colour
      IColor bg = this.background.GetBackgroundColor(r);
      return new PathtracerLightPath(bg, null);
    }
  }
}
