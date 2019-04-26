using System.IO;
using System.Threading.Tasks;
using dyim.RayTracer.Color;

namespace dyim.RayTracer.Image
{
  public static class SensorPPMExtension
  {
    public static async Task WritePPMFile(this Sensor sensor, string filePath)
    {
      using (StreamWriter writer = new StreamWriter(filePath))
      {
        await writer.WriteAsync($"P3\n{sensor.Width} {sensor.Height}\n255\n");
        for (int j = sensor.Height - 1; j >= 0; j--)
        {
          for (int i = 0; i < sensor.Width; i++)
          {
              IColor c = sensor.GetColorSpaceValue(i, j);
              int ir = (int)(255.99 * c.R);
              int ig = (int)(255.99 * c.G);
              int ib = (int)(255.99 * c.B);

              await writer.WriteAsync($"{ir} {ig} {ib}\n");
          }
        }
      }
    }
  }
}
