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

    /// <summary>
    /// Creates a camera.
    /// </summary>
    /// <param name="lookFrom">The point where the camera is located</param>
    /// <param name="lookAt">The point that the camera is looking at</param>
    /// <param name="up">The up direction</param>
    /// <param name="verticalFov">Vertical field of view in radians</param>
    /// <param name="aspectRatio">width over height</param>
    public Camera(
      Vector3 lookFrom,
      Vector3 lookAt,
      Vector3 up,
      double verticalFov,
      double aspectRatio)
    {
      double halfHeight = Math.Atan(verticalFov / 2.0);
      double halfWidth = aspectRatio * halfHeight;

      this.origin = lookFrom;
      Vector3 w = (lookFrom - lookAt).ToUnitVector();
      Vector3 u = up.Cross(w).ToUnitVector();
      Vector3 v = w.Cross(u);

      this.lowerLeftCorner = this.origin - halfWidth * u - halfHeight * v - w;
      this.horizontal = 2.0 * halfWidth * u;
      this.vertical = 2.0 * halfHeight * v;
    }

    public Ray3 GetRay(double u, double v)
    {
      return new Ray3(
        this.origin, 
        this.lowerLeftCorner + u * this.horizontal + v * this.vertical - this.origin);
    }
  }
}
