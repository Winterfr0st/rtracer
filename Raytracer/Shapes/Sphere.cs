using System;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Shapes
{
  public class Sphere : IHitable
  {
    private readonly Vector3 center;
    private readonly double radius;
    private readonly IMaterial material;

    public Sphere(Vector3 center, double radius, IMaterial material)
    {
      this.center = center;
      this.radius = radius;
      this.material = material;
    }

    public Sphere(Sphere other)
      : this(other.center, other.radius, other.material)
    {
    }

    public HitRecord Hit(Ray3 r, double tMin, double tMax)
    {
      Vector3 oc = r.Origin - this.center;

      double a = r.Direction.Dot(r.Direction);
      double b = 2.0 * oc.Dot(r.Direction);
      double c = oc.Dot(oc) - this.radius * this.radius;
      double discriminant = b * b - 4 * a * c;

      if (discriminant > 0)
      {
        double temp = (-b - System.Math.Sqrt(discriminant)) / (2.0 * a);
        if (temp < tMax && temp > tMin)
        {
          Vector3 p = r.PointAtParameter(temp);
          HitRecord record = new HitRecord(
            temp,
            p,
            (p - this.center) / this.radius,
            material);

          return record;
        }

        temp = (-b + System.Math.Sqrt(discriminant)) / (2.0 * a);
        if (temp < tMax && temp > tMin)
        {
          Vector3 p = r.PointAtParameter(temp);
          HitRecord record = new HitRecord(
            temp,
            p,
            (p - this.center) / this.radius,
            material);

          return record;
        }
      }

      return null;
    }
  }
}
