using System;
using System.Collections.Generic;
using System.Text;
using dyim.RayTracer.Color;

namespace dyim.RayTracer.Image
{
  public class SqrtColorSpace : IColorSpace
  {
    public RGBColor RawToColorSpace(IColor color)
    {
      double r = Math.Sqrt(color.R);
      double g = Math.Sqrt(color.G);
      double b = Math.Sqrt(color.B);

      return new RGBColor(r, g, b);
    }
  }
}
