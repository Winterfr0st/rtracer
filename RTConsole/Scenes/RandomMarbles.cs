using System;
using System.Collections.Generic;
using dyim.RayTracer;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace RaytracerCSharp
{
  public class RandomMarbles : IScene
  {
    public void RenderScene()
    {
      int nx = 800;
      int ny = 400;
      int ns = 100;

      Console.WriteLine("P3\n{0} {1}\n255\n", nx, ny);

      Random rng = new Random();
      UnitCircleUniformSampler circleSampler = new UnitCircleUniformSampler(rng);

      HitableList world = this.GenerateWorld(rng);
      Console.Error.WriteLine("Generated the world");

      Vector3 lookFrom = new Vector3(8, 2, 1.5);
      Vector3 lookAt = new Vector3(0, 0.0, 0);
      Camera camera = new Camera(
        circleSampler,
        lookFrom,
        lookAt,
        new Vector3(0, 1, 0),
        45.0 * Math.PI / 180.0,
        (double)nx / (double)ny,
        0.005,
        (lookAt - lookFrom).Length());

      Random random = new Random();
      for (int j = ny - 1; j >= 0; j--)
      {
        Console.Error.WriteLine($"y: {j}");
        for (int i = 0; i < nx; i++)
        {
          Vector3 color = new Vector3(0, 0, 0);

          for (int s = 0; s < ns; ++s)
          {
            double u = (i + random.NextDouble()) / nx;
            double v = (j + random.NextDouble()) / ny;

            Ray3 r = new Ray3(camera.GetRay(u, v));
            color += Program.Color(r, world, 0);
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

    private HitableList GenerateWorld(Random rng)
    {
      UnitSphereUniformSampler sphereSampler = new UnitSphereUniformSampler(rng);

      List<IHitable> objects = new List<IHitable>();

      // Add the main earth object
      IMaterial earthMaterial = new Lambertian(new Vector3(0.5, 0.5, 0.5), sphereSampler);
      objects.Add(new Sphere(new Vector3(0, -1000, 0), 1000, earthMaterial));

      // Generate random objects
      Vector3 temp = new Vector3(4, 0.2, 0);
      for (int a = -11; a < 11; ++a)
      {
        for (int b = -11; b < 11; ++b)
        {
          double chooseMat = rng.NextDouble();
          Vector3 center = new Vector3(a + 0.9 * rng.NextDouble(), 0.2, b + 0.9 * rng.NextDouble());
          if ((center - temp).Length() > 0.9)
          {
            // 80% chance to choose diffuse
            if (chooseMat < 0.8)
            {
              double red = rng.NextDouble() * rng.NextDouble();
              double green = rng.NextDouble() * rng.NextDouble();
              double blue = rng.NextDouble() * rng.NextDouble();

              var randomDiffuseMaterial = new Lambertian(
                new Vector3(red, green, blue),
                sphereSampler);
              objects.Add(new Sphere(center, 0.2, randomDiffuseMaterial));
            }
            else if (chooseMat < 0.95)
            {
              // 15% chance for metal material
              double red = 0.5 * (1 + rng.NextDouble());
              double green = 0.5 * (1 + rng.NextDouble());
              double blue = 0.5 * (1 + rng.NextDouble());
              double fuzz = 0.5 * rng.NextDouble();
              var randomMetal = new Metal(new Vector3(red, green, blue), fuzz, sphereSampler);
              objects.Add(new Sphere(center, 0.2, randomMetal));
            }
            else
            {
              // 5% chance for dielectric
              objects.Add(new Sphere(center, 0.2, new Dielectric(1.5, rng)));
            }
          }
        }
      }

      objects.Add(new Sphere(new Vector3(0, 1, 0), 1.0, new Dielectric(1.5, rng)));
      objects.Add(new Sphere(new Vector3(-4, 1, 0), 1.0, new Lambertian(new Vector3(0.4, 0.2, 0.1), sphereSampler)));
      objects.Add(new Sphere(new Vector3(4, 1, 0), 1.0, new Metal(new Vector3(0.7, 0.6, 0.5), 0.0, sphereSampler)));

      return new HitableList(objects);
    }
  }
}
