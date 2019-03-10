using System;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer
{
  public class Camera
  {
    private readonly UnitCircleUniformSampler rng;
    private readonly Vector3 origin;
    private readonly Vector3 lowerLeftCorner;
    private readonly Vector3 horizontal;
    private readonly Vector3 vertical;
    private readonly Vector3 u, v, w;
    private readonly double lensRadius;

    /// <summary>
    /// Creates a camera.
    /// </summary>
    /// <param name="lookFrom">The point where the camera is located</param>
    /// <param name="lookAt">The point that the camera is looking at</param>
    /// <param name="up">The up direction</param>
    /// <param name="verticalFov">Vertical field of view in radians</param>
    /// <param name="aspectRatio">width over height</param>
    public Camera(
      UnitCircleUniformSampler rng,
      Vector3 lookFrom,
      Vector3 lookAt,
      Vector3 up,
      double verticalFov,
      double aspectRatio,
      double aperature,
      double focusDist)
    {
      this.rng = rng;

      this.lensRadius = aperature / 2.0;
      double halfHeight = Math.Atan(verticalFov / 2.0);
      double halfWidth = aspectRatio * halfHeight;

      this.origin = lookFrom;
      this.w = (lookFrom - lookAt).ToUnitVector();
      this.u = up.Cross(w).ToUnitVector();
      this.v = w.Cross(u);

      this.lowerLeftCorner = this.origin - halfWidth * focusDist * u - halfHeight * focusDist * v - focusDist * w;
      this.horizontal = 2.0 * halfWidth * focusDist * u;
      this.vertical = 2.0 * halfHeight * focusDist * v;
    }

    public Ray3 GetRay(double s, double t)
    {
      Vector3 randPointInAperature = rng.GenerateSample() * lensRadius;
      Vector3 offset = this.u * randPointInAperature.X + this.v * randPointInAperature.Y;

      Vector3 newOrigin = offset + this.origin;

      return new Ray3(
        newOrigin, 
        this.lowerLeftCorner + s * this.horizontal + t * this.vertical - newOrigin);
    }
  }
}
