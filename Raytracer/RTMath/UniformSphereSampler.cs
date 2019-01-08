using System;
namespace dyim.RayTracer.RTMath
{
  public class UniformSphereSampler
  {
    private readonly Random r;

    public UniformSphereSampler()
    {
      this.r = new Random();
    }

    public Vector3 GenerateSample()
    {
      Vector3 p;
      Vector3 o = new Vector3(1,1,1);
      
      do
      {
        double x = this.r.NextDouble();
        double y = this.r.NextDouble();
        double z = this.r.NextDouble();

        p = 2.0 * new Vector3(x, y, z) - o;
      }
      while (p.SquaredLength() >= 1.0);

      return p;
    }
  }
}
