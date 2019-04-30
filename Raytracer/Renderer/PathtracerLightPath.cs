using System;
using System.Collections.Generic;
using System.Text;
using dyim.RayTracer.Color;
using dyim.RayTracer.Material;

namespace dyim.RayTracer.Renderer
{
  public class PathtracerLightPath : ILightPath
  {
    private IColor color;
    private ILightPath next;

    public PathtracerLightPath(IColor color, ILightPath next)
    {
      this.next = next;
      this.color = color;
    }

    public IColor Calculate()
    {
      if (this.next != null)
      {
        return this.color.Multiply(this.next.Calculate());
      }

      return this.color;
    }
  }
}
