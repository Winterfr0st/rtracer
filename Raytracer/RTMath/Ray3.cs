using System;
namespace dyim.RayTracer.RTMath
{
  public class Ray3
  {
    private readonly Vector3 origin;
    private readonly Vector3 direction;

    public Ray3(Vector3 origin, Vector3 direction)
    {
      this.origin = origin;
      this.direction = direction;
    }

    public Ray3(Ray3 other)
      : this(other.origin, other.direction)
    {
    }

    public Vector3 Origin => this.origin;

    public Vector3 Direction => this.direction;

    public Vector3 A => this.origin;

    public Vector3 B => this.direction;

    public Vector3 PointAtParameter(double t)
    {
      return this.origin + t * this.direction;
    }
  }
}
