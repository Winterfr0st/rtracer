using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Material
{
  public class ScatterRecord
  {
    public ScatterRecord(Vector3 attenuation, Ray3 scatter)
    {
      this.Attenuation = attenuation;
      this.Scatter = scatter;
    }

    public ScatterRecord(ScatterRecord other)
      : this(other.Attenuation, other.Scatter)
    {
    }

    public Vector3 Attenuation { get; }

    public Ray3 Scatter { get; }
  }
}
