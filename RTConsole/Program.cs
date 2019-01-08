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

    private static Vector3 Color(Ray3 r, HitableList world, int depth)
    {
      HitRecord record = world.Hit(r, 0.001, 10000);
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

        return new Vector3(0, 0, 0);
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

      UniformSphereSampler sampler = new UniformSphereSampler();

      IMaterial mat1 = new Lambertian(new Vector3(0.8, 0.3, 0.3), sampler);
      IMaterial mat2 = new Lambertian(new Vector3(0.8, 0.8, 0.0), sampler);
      IMaterial mat3 = new Metal(new Vector3(0.8, 0.6, 0.2), 0.3, sampler);
      IMaterial mat4 = new Metal(new Vector3(0.8, 0.8, 0.8), 1.0, sampler);

      List<IHitable> objects = new List<IHitable>(4);
      objects.Add(new Sphere(new Vector3(0, 0, -1), 0.5, mat1));
      objects.Add(new Sphere(new Vector3(0, -100.5, -1), 100, mat2));
      objects.Add(new Sphere(new Vector3(1, 0, -1), 0.5, mat3));
      objects.Add(new Sphere(new Vector3(-1, 0, -1), 0.5, mat4));

      HitableList world = new HitableList(objects);
      Camera camera = new Camera(
        new Vector3(0.0, 0.0, 0.0),
        new Vector3(-2.0, -1.0, -1.0),
        new Vector3(4.0, 0.0, 0.0),
        new Vector3(0.0, 2.0, 0.0));

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
