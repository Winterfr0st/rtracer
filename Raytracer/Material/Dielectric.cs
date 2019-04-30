using System;
using dyim.RayTracer.Color;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Material
{
  public class Dielectric : IMaterial
  {
    private readonly double refractiveIndex;
    private readonly Random rng;
    private readonly IColor attenuation;

    public Dielectric(double refractiveIndex, IColor attenuation, Random rng)
    {
      this.refractiveIndex = refractiveIndex;
      this.rng = rng;
      this.attenuation = attenuation;
    }

    public ScatterRecord Scatter(Ray3 rIn, HitRecord record)
    {
      Vector3 outwardNormal;
      double ni;
      double nt;
      double cosine;

      // Assumes that refractive index of material outside of this material is air i.e. 1.0
      if (rIn.Direction.Dot(record.Normal) > 0)
      {
        // Ray is going from inside the material to outside so the normal is facing the wrong
        // direction for the purpose of our refract function. So flip the normal.
        outwardNormal = -record.Normal;
        ni = this.refractiveIndex;
        nt = 1.0;

        /*
        cosine = rIn.Direction.Dot(record.Normal) / rIn.Direction.Length();
        cosine = Math.Sqrt(1.0 - this.refractiveIndex * this.refractiveIndex * (1.0 - cosine * cosine));
        */

        cosine = this.refractiveIndex * rIn.Direction.Dot(record.Normal) / rIn.Direction.Length();
      }
      else
      {
        outwardNormal = record.Normal;
        ni = 1.0;
        nt = this.refractiveIndex;

        cosine = -(rIn.Direction.Dot(record.Normal)) / rIn.Direction.Length();
      }

      // Check if this should reflect or refract
      Vector3 refractedDir = Physics.Refract(rIn.Direction, outwardNormal, ni, nt);
      double reflectProbability;
      if (refractedDir != null)
      {
        reflectProbability = Physics.Schlick(cosine, this.refractiveIndex);
      }         
      else
      {
        reflectProbability = 1.0;
      }

      if (rng.NextDouble() < reflectProbability)
      {
        // Reflects
        Vector3 reflectedDir = Physics.Reflect(rIn.Direction, outwardNormal);
        return new ScatterRecord(this.attenuation, new Ray3(record.Point, reflectedDir));
      }
      else
      {
        // Ray is refracted
        return new ScatterRecord(this.attenuation, new Ray3(record.Point, refractedDir));
      }
    }
  }
}
