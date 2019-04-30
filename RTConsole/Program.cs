using System;
using System.Diagnostics;
using dyim.RayTracer.Color;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace RaytracerCSharp
{
  public static class Program
  {
    private static readonly IColor WhiteColor = new RGBColor(1.0, 1.0, 1.0);
    private static readonly IColor SkyColor = new RGBColor(0.5, 0.7, 1.0);
    private static readonly IColor BlackColor = new RGBColor(0, 0, 0);

    internal static IColor Color(Ray3 r, HitableList world, int depth)
    {
      HitRecord record = world.Hit(r, 0.01, 10000);
      if (null != record)
      {
        if (depth < 50)
        {
          ScatterRecord sr = record.Material.Scatter(r, record);
          if (null != sr)
          {
            return sr.Attenuation.Multiply(Color(sr.Scatter, world, depth + 1));
          }
        }

        // Either got absorbed by material or this ray refracted/reflected 50 times.
        // Don't want to go more than 50 depth so stop here in that case.
        return BlackColor;
      }

      Vector3 unitDir = r.Direction.ToUnitVector();
      double t = 0.5 * (unitDir.Y + 1.0);
      return WhiteColor.Multiply(1.0 - t).Add(SkyColor.Multiply(t));
    }

    public static void Main(string[] args)
    {
      Stopwatch watch = Stopwatch.StartNew();
      IScene scene = new ThreeSpheres(); //// new RandomMarbles();
      scene.RenderScene();
      watch.Stop();

      Console.WriteLine(watch.ElapsedMilliseconds + "ms");
    }
  }
}
