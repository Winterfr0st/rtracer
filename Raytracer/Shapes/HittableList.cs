using System;
using System.Collections.Generic;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Shapes
{
  public class HitableList : IHitable
  {
    private readonly IEnumerable<IHitable> list;

    public HitableList(IEnumerable<IHitable> list)
    {
      this.list = list;
    }

    public HitableList(HitableList other)
      : this(other.list)
    {
    }

    public HitRecord Hit(Ray3 r, double tMin, double tMax)
    {
      HitRecord closest = null;
      double closestSoFar = tMax;

      foreach (IHitable item in list)
      {
        HitRecord record = item.Hit(r, tMin, closestSoFar);
        if (null != record)
        {
          closestSoFar = record.T;
          closest = record;
        }
      }

      return closest;
    }
  }
}
