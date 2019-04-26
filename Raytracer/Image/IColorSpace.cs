using dyim.RayTracer.Color;

namespace dyim.RayTracer.Image
{
  public interface IColorSpace
  {
    RGBColor RawToColorSpace(IColor color);
  }
}
