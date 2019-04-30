using dyim.RayTracer.Color;
using dyim.RayTracer.RTMath;

namespace dyim.RayTracer.Material
{
  public class ScatterRecord
  {
    public ScatterRecord(IColor attenuation, Ray3 scatter)
    {
      this.Attenuation = attenuation;
      this.Scatter = scatter;
    }

    public ScatterRecord(ScatterRecord other)
      : this(other.Attenuation, other.Scatter)
    {
    }

    public IColor Attenuation { get; }

    public Ray3 Scatter { get; }
  }
}
