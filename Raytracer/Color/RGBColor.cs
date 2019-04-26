using System;
using System.Collections.Generic;
using System.Text;

namespace dyim.RayTracer.Color
{
  public class RGBColor : IColor
  {
    public static readonly RGBColor Black = new RGBColor(0, 0, 0);
    public static readonly RGBColor White = new RGBColor(1, 1, 1);

    private double r;
    private double g;
    private double b;

    public RGBColor(double r, double g, double b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public double R => this.r;

    public double G => this.g;

    public double B => this.b;

    public IColor Add(IColor other)
    {
        var rgbOther = other as RGBColor;
        return new RGBColor(
            this.r + rgbOther.r,
            this.g + rgbOther.g,
            this.b + rgbOther.b);
    }

    public IColor Multiply(IColor other)
    {
        var rgbOther = other as RGBColor;
        return new RGBColor(
            this.r * rgbOther.r,
            this.g * rgbOther.g,
            this.b * rgbOther.b);
    }

    public IColor Multiply(double t)
    {
        return new RGBColor(
            this.r * t,
            this.g * t,
            this.b * t);
    }
  }
}
