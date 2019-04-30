using System;
using System.Diagnostics;
using dyim.RayTracer.Color;
using dyim.RayTracer.Material;
using dyim.RayTracer.Renderer;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace RaytracerCSharp
{
  public static class Program
  {
    private static readonly IColor WhiteColor = new RGBColor(1.0, 1.0, 1.0);
    private static readonly IColor SkyColor = new RGBColor(0.5, 0.7, 1.0);
    private static readonly IColor BlackColor = new RGBColor(0, 0, 0);

    internal static ILightPath Color(Ray3 r, HitableList world, int depth)
    {
      HitRecord record = world.Hit(r, 0.01, 10000);
      if (null != record)
      {
        if (depth < 50)
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
        return new PathtracerLightPath(BlackColor, null);
      }

      Vector3 unitDir = r.Direction.ToUnitVector();
      double t = 0.5 * (unitDir.Y + 1.0);
      IColor skylight = WhiteColor.Multiply(1.0 - t).Add(SkyColor.Multiply(t));
      return new PathtracerLightPath(skylight, null);
    }

    public static void Main(string[] args)
    {
      Stopwatch watch = Stopwatch.StartNew();
      IScene scene = new ThreeSpheres(); //// new RandomMarbles();
      scene.RenderScene();
      watch.Stop();

      Console.WriteLine();
      Console.WriteLine(watch.ElapsedMilliseconds + "ms");
    }
  }
}
