using System;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Material
{
  public static class Physics
  {
    // Returns the reflection direction given the incoming vector and
    // the normalÏ
    public static Vector3 Reflect(Vector3 v, Vector3 normal)
    {
      return v - 2 * v.Dot(normal) * normal;
    }

    // Refracts the given ray going in direction v when it hits surface with the given normal,
    // and where ni is the index of refraction of the first material and nt is the index of
    // refraction of the second material. Returns true if there is refraction and refractedDir
    // is the direction the ray is refracted.
    public static Vector3 Refract(Vector3 v, Vector3 normal, double ni, double nt)
    {
      double niOverNt = ni / nt;

      Vector3 normalizedV = v.ToUnitVector();
      double dt = normalizedV.Dot(normal);
      double descriminant = 1.0 - niOverNt * niOverNt * (1.0 - dt * dt);
      if (descriminant > 0)
      {
        return niOverNt * (normalizedV - normal * dt) - normal * Math.Sqrt(descriminant);
      }
      else
      {
        return null;
      }
    }

    // Approximation of real glass reflectivity made by Christophe Schlick.
    public static double Schlick(double cosine, double refractiveIndex)
    {
      double r0 = (1.0 - refractiveIndex) / (1.0 + refractiveIndex);
      r0 = r0 * r0;
      return r0 + (1.0 - r0) * Math.Pow((1.0 - cosine), 5);
    }
  }
}
