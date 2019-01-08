using System;

namespace dyim.RayTracer.RTMath
{
  public static class Utility
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
    public static bool Refract(Vector3 v, Vector3 normal, double ni, double nt, Vector3 refractedDir)
    {
      double niOverNt = ni / nt;

      Vector3 normalizedV = v.ToUnitVector();
      double dt = normalizedV.Dot(normal);
      double descriminant = 1.0 - niOverNt * niOverNt * (1.0 - dt * dt);
      if (descriminant > 0)
      {
        refractedDir = niOverNt * (v - normal * dt) - normal * System.Math.Sqrt(descriminant);
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
