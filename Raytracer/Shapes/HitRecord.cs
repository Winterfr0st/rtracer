using System;
using dyim.RayTracer.Material;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Shapes
{
  public class HitRecord
  {
    public HitRecord(double t, Vector3 point, Vector3 normal, IMaterial material)
    {
      this.T = t;
      this.Point = point;
      this.Normal = normal;
      this.Material = material;
    }

    public double T { get; }
    public Vector3 Point { get; }
    public Vector3 Normal { get; }
    public IMaterial Material { get; }
  }
}
