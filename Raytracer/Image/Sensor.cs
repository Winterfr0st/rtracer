using dyim.RayTracer.Color;

namespace dyim.RayTracer.Image
{
  public class Sensor
  {
    private readonly IColor[,] image;
    private readonly int[,] numSamplesPerPixel;
    private readonly int width;
    private readonly int height;

    private readonly IColorSpace colorSpace;

    public Sensor(int width, int height, IColorSpace colorSpace, IColor initialValue)
    {
      this.image = new IColor[height, width];
      this.numSamplesPerPixel = new int[height, width];

      for (int y = 0; y < height; ++y)
      {
        for (int x = 0; x < width; ++x)
        {
          this.numSamplesPerPixel[y, x] = 0;
          this.image[y, x] = initialValue;
        }
      }

      this.width = width;
      this.height = height;
      this.colorSpace = colorSpace;
    }

    public int Width => this.width;

    public int Height => this.height;

    public IColor GetRawValue(int x, int y)
    {
      if (this.numSamplesPerPixel[y, x] <= 1)
      {
        return this.image[y, x];
      }

      return this.image[y, x].Multiply(1.0 / this.numSamplesPerPixel[y, x]);
    }

    public IColor GetColorSpaceValue(int x, int y)
    {
      IColor raw = this.GetRawValue(x, y);
      if (this.colorSpace == null)
      {
        return raw;
      }

      return this.colorSpace.RawToColorSpace(raw);
    }

    public void SetValue(int x, int y, IColor value)
    {
      this.image[y, x] = value;
      this.numSamplesPerPixel[y, x] = 1;
    }

    public void AddSample(int x, int y, IColor value)
    {
      this.image[y, x] = this.image[y, x].Add(value);
      this.numSamplesPerPixel[y, x] += 1;
    }
  }
}
