using System;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Material
{
  public class Metal : IMaterial
  {
    private readonly Vector3 albedo;
    private readonly double fuzz;
    private readonly UniformSphereSampler sampler;

    public Metal(Vector3 a, double fuzz, UniformSphereSampler sampler)
    {
      this.albedo = a;
      this.fuzz = fuzz;
      this.sampler = sampler;
    }

    public Metal(Metal other)
      : this(other.albedo, other.fuzz, other.sampler)
    {
    }

    public ScatterRecord Scatter(Ray3 rIn, HitRecord record)
    {
      Vector3 reflected = Utility.Reflect(rIn.Direction.ToUnitVector(), record.Normal);
      reflected = reflected + this.fuzz * this.sampler.GenerateSample();

      Ray3 scatter = new Ray3(record.Point, reflected);

      if (scatter.Direction.Dot(record.Normal) <= 0)
      {
        return null;
      }

      return new ScatterRecord(this.albedo, scatter);
    }
  }
}
