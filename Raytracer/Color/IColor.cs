using System;
using System.Collections.Generic;
using System.Text;

namespace dyim.RayTracer.Color
{
  public interface IColor
  {
    double R { get; }

    double G { get; }

    double B { get; }

    IColor Add(IColor other);

    IColor Multiply(IColor other);

    IColor Multiply(double t);
  }
}
