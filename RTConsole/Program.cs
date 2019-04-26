using System;
using System.Collections.Generic;
using dyim.RayTracer;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace RaytracerCSharp
{
  public static class Program
  {
    private static readonly Vector3 WhiteColor = new Vector3(1.0, 1.0, 1.0);
    private static readonly Vector3 SkyColor = new Vector3(0.5, 0.7, 1.0);
    private static readonly Vector3 BlackColor = new Vector3(0, 0, 0);

    internal static Vector3 Color(Ray3 r, HitableList world, int depth)
    {
      HitRecord record = world.Hit(r, 0.01, 10000);
      if (null != record)
      {
        if (depth < 50)
        {
          ScatterRecord sr = record.Material.Scatter(r, record);
          if (null != sr)
          {
            return sr.Attenuation * Color(sr.Scatter, world, depth + 1);
          }
        }

        // Either got absorbed by material or this ray refracted/reflected 50 times.
        // Don't want to go more than 50 depth so stop here in that case.
        return BlackColor;
      }

      Vector3 unitDir = r.Direction.ToUnitVector();
      double t = 0.5 * (unitDir.Y + 1.0);
      return (1.0 - t) * WhiteColor + t * SkyColor;
    }

    public static void Main(string[] args)
    {
      IScene scene = new RandomMarbles();
      scene.RenderScene();
    }
  }
}
