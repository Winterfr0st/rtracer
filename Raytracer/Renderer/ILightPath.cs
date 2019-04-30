using System;
using System.Collections.Generic;
using dyim.RayTracer.Color;
using dyim.RayTracer.RTMath;
using dyim.RayTracer.Shapes;

namespace dyim.RayTracer.Renderer
{
  public interface ILightPath
  {
    /// <summary>
    /// Calculates the color result for ray tracing along this light path
    /// </summary>
    /// <returns>The color</returns>
    IColor Calculate();
  }
}
