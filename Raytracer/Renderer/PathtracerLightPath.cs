using System;
using System.Collections.Generic;
using System.Text;
using dyim.RayTracer.Color;
using dyim.RayTracer.Material;

namespace dyim.RayTracer.Renderer
{
  public class PathtracerLightPath : ILightPath
  {
    private ScatterRecord sr;
    private PathtracerLightPath next;

    public PathtracerLightPath(ScatterRecord scatterRecord, PathtracerLightPath next)
    {
      this.next = next;
      this.sr = scatterRecord;
    }

    public IColor Calculate()
    {
      throw new NotImplementedException();
    }
  }
}
