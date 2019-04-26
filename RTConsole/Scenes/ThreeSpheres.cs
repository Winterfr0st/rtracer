using System;
using System.Collections.Generic;
using System.IO;
using dyim.RayTracer;
using dyim.RayTracer.Color;
using dyim.RayTracer.Image;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace RaytracerCSharp
{
  public class ThreeSpheres : IScene
  {
    public ThreeSpheres()
    {
    }

    public void RenderScene()
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

      // Create folder to output frames
      if (!Directory.Exists("ThreeSpheres"))
      {
        Directory.CreateDirectory("ThreeSpheres");
      }

      Sensor sensor = new Sensor(nx, ny, new SqrtColorSpace());
      int frameNum = 0;
      for (int s = 0; s < ns; ++s)
      {
        for (int j = ny - 1; j >= 0; --j)
        {
          for (int i = 0; i < nx; i++)
          {
            double u = (i + random.NextDouble()) / nx;
            double v = (j + random.NextDouble()) / ny;

            Ray3 r = new Ray3(camera.GetRay(u, v));
            Vector3 color = Program.Color(r, world, 0);
            sensor.AddSample(i, j, new RGBColor(color[0], color[1], color[2]));
          }
        }


        if (s % (ns / 10) == 0)
        {
          // Output 1 frame with current number of samples
          sensor.WritePPMFile($"ThreeSpheres\frame_{frameNum}.ppm").Wait();
          frameNum++;
        }
      }

      sensor.WritePPMFile("finalOutput.ppm").Wait();
    }
  }
}
