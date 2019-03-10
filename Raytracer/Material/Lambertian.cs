using System;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Material
{
  public class Lambertian : IMaterial
  {
    private readonly Vector3 albedo;
    private readonly UnitSphereUniformSampler sampler;

    public Lambertian(Vector3 albedo, UnitSphereUniformSampler sampler)
    {
      this.albedo = albedo;
      this.sampler = sampler;
    }

    public Lambertian(Lambertian other)
      : this(other.albedo, other.sampler)
    {
    }

    public ScatterRecord Scatter(Ray3 rIn, HitRecord record)
    {
      Vector3 target = record.Point + record.Normal + sampler.GenerateSample();

      return
        new ScatterRecord(this.albedo, new Ray3(record.Point, target - record.Point));
    }
  }
}
