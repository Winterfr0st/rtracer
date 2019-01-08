using System;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer
{
  public class Camera
  {
    private readonly Vector3 origin;
    private readonly Vector3 lowerLeftCorner;
    private readonly Vector3 horizontal;
    private readonly Vector3 vertical;

    public Camera(Vector3 origin, Vector3 lowerLeftCorner, Vector3 horizontal, Vector3 vertical)
    {
      this.origin = origin;
      this.lowerLeftCorner = lowerLeftCorner;
      this.horizontal = horizontal;
      this.vertical = vertical;
    }

    public Ray3 GetRay(double u, double v)
    {
      return new Ray3(
        this.origin, 
        this.lowerLeftCorner + u * this.horizontal + v * this.vertical - this.origin);
    }
  }
}
