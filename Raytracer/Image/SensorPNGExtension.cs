using System.IO;
using System.Threading.Tasks;
using dyim.RayTracer.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace dyim.RayTracer.Image
{
  public static class SensorPNGExtension
  {
    public static void WritePNGFile(this Sensor sensor, string filePath)
    {
      using (Image<Rgba32> image = new Image<Rgba32>(sensor.Width, sensor.Height))
      {
        for (int j = sensor.Height - 1; j >= 0; j--)
        {
          for (int i = 0; i < sensor.Width; i++)
          {
            IColor c = sensor.GetColorSpaceValue(i, j);
            byte ir = (byte)(255.99 * c.R);
            byte ig = (byte)(255.99 * c.G);
            byte ib = (byte)(255.99 * c.B);

            image[i, (sensor.Height - 1) - j] = new Rgba32(ir, ig, ib);
          }
        }

        image.Save(filePath);
      }
    }
  }
}
