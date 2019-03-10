using System;
namespace dyim.RayTracer.RTMath
{
  public class UnitCircleUniformSampler
  {
    private readonly Random r;

    public UnitCircleUniformSampler()
    {
      this.r = new Random();
    }

    public UnitCircleUniformSampler(Random rng)
    {
      this.r = rng;
    }

    public UnitCircleUniformSampler(UnitCircleUniformSampler other)
    {
      this.r = other.r;
    }

    public Vector3 GenerateSample()
    {
      Vector3 p;
      Vector3 o = new Vector3(1, 1, 0);

      do
      {
        double x = this.r.NextDouble();
        double y = this.r.NextDouble();
        double z = 0;

        p = 2.0 * new Vector3(x, y, z) - o;
      }
      while (p.Dot(p) >= 1.0);

      return p;
    }
  }
}
