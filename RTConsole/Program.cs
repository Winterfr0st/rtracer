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

    private static Vector3 Color(Ray3 r, HitableList world, int depth)
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
      int nx = 800;
      int ny = 400;
      int ns = 100;

      Console.WriteLine("P3\n{0} {1}\n255\n", nx, ny);

      var lowerLeftCorner = new Vector3(-2.0, -1.0, -1.0);
      var horizontal = new Vector3(4.0, 0.0, 0.0);
      var vertical = new Vector3(0.0, 2.0, 0.0);
      var origin = new Vector3(0.0, 0.0, 0.0);

      Random rng = new Random();
      UnitSphereUniformSampler sphereSampler = new UnitSphereUniformSampler(rng);
      UnitCircleUniformSampler circleSampler = new UnitCircleUniformSampler(rng);

      IMaterial mat1 = new Lambertian(new Vector3(0.1, 0.2, 0.5), sphereSampler);
      IMaterial mat2 = new Lambertian(new Vector3(0.8, 0.8, 0.0), sphereSampler);
      IMaterial mat3 = new Metal(new Vector3(0.8, 0.6, 0.2), 0.3, sphereSampler);
      IMaterial mat4 = new Dielectric(1.5, rng);

      List<IHitable> objects = new List<IHitable>(4);
      objects.Add(new Sphere(new Vector3(0, 0, -1), 0.5, mat1));
      objects.Add(new Sphere(new Vector3(0, -100.5, -1), 100, mat2));
      objects.Add(new Sphere(new Vector3(1, 0, -1), 0.5, mat3));
      objects.Add(new Sphere(new Vector3(-1, 0, -1), 0.5, mat4));
      objects.Add(new Sphere(new Vector3(-1, 0, -1), -0.45, mat4));

      HitableList world = new HitableList(objects);
      Vector3 lookFrom = new Vector3(-2, 2, 1);
      Vector3 lookAt = new Vector3(0, 0, -1);
      Camera camera = new Camera(
        circleSampler,
        lookFrom,
        lookAt,
        new Vector3(0, 1, 0),
        90.0 * Math.PI / 180.0,
        (double)nx / (double)ny,
        0.2,
        (lookAt - lookFrom).Length());

      Random random = new Random();
      for (int j = ny - 1; j >= 0; j--)
      {
        for (int i = 0; i < nx; i++)
        {
          Vector3 color = new Vector3(0, 0, 0);

          for (int s = 0; s < ns; ++s)
          {
            double u = (i + random.NextDouble()) / nx;
            double v = (j + random.NextDouble()) / ny;

            Ray3 r = new Ray3(camera.GetRay(u, v));
            color += Color(r, world, 0);
          }

          color /= ns;
          color = new Vector3(Math.Sqrt(color[0]), Math.Sqrt(color[1]), Math.Sqrt(color[2]));

          int ir = (int)(255.99 * color[0]);
          int ig = (int)(255.99 * color[1]);
          int ib = (int)(255.99 * color[2]);

          Console.WriteLine("{0} {1} {2}", ir, ig, ib);
        }
      }
    }
  }
}
