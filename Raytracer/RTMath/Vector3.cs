//--------------------------------------------
// (c) Daniel Yim
//--------------------------------------------
using System;

namespace dyim.RayTracer.RTMath
{
  public sealed class Vector3
  {
    private readonly double e0;
    private readonly double e1;
    private readonly double e2;

    public Vector3(double e0, double e1, double e2)
    {
      this.e0 = e0;
      this.e1 = e1;
      this.e2 = e2;
    }

    public Vector3(Vector3 other)
      : this(other.e0, other.e1, other.e2)
    {
    }

    #region Accessors
    public double X => this.e0;

    public double Y => this.e1;

    public double Z => this.e2;

    public double R => this.e0;

    public double G => this.e1;

    public double B => this.e2;

    public double this[int index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return this.e0;
          case 1:
            return this.e1;
          case 2:
            return this.e2;
          default:
            throw new ArgumentOutOfRangeException(nameof(index));
        }
      }
    }
    #endregion

    #region Common Vector Operations
    public double Length()
    {
      return System.Math.Sqrt(
        this.e0 * this.e0 +
        this.e1 * this.e1 +
        this.e2 * this.e2);
    }

    public double SquaredLength()
    {
      return this.e0 * this.e0 +
             this.e1 * this.e1 +
             this.e2 * this.e2;
    }

    public Vector3 ToUnitVector()
    {
      double k = 1.0 / this.Length();
      return new Vector3(this.e0 * k, this.e1 * k, this.e2 * k);
    }

    public double Dot(Vector3 v)
    {
      return this.X * v.X + this.Y * v.Y + this.Z * v.Z;
    }

    public Vector3 Cross(Vector3 v)
    {
      return new Vector3(
        this.Y * v.Z - this.Z * v.Y,
        -(this.X * v.Z - this.Z * v.X),
        this.X * v.Y - this.Y * v.X);
    }
    #endregion

    #region Operator Overrides for Common Vector Operations
    public static Vector3 operator+(Vector3 v)
    {
      return v;
    }

    public static Vector3 operator-(Vector3 v)
    {
      return new Vector3(-v.X, -v.Y, -v.Z);
    }

    public static Vector3 operator+(Vector3 v1, Vector3 v2)
    {
      return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3 operator-(Vector3 v1, Vector3 v2)
    {
      return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector3 operator*(Vector3 v1, Vector3 v2)
    {
      return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
    }

    public static Vector3 operator/(Vector3 v1, Vector3 v2)
    {
      return new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
    }

    public static Vector3 operator*(Vector3 v, double t)
    {
      return new Vector3(v.X * t, v.Y * t, v.Z * t);
    }

    public static Vector3 operator*(double t, Vector3 v)
    {
      return new Vector3(v.X * t, v.Y * t, v.Z * t);
    }

    public static Vector3 operator/(Vector3 v, double t)
    {
      return new Vector3(v.X / t, v.Y / t, v.Z / t);
    }
    #endregion
  }
}


