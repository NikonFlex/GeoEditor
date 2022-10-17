using System;

namespace GE_Primitive
{
   class PrimPoint : System.ICloneable
   {
      public double X { get; set; } = 0;
      public double Y { get; set; } = 0;

      public PrimPoint(double x = 0, double y = 0)
      {
         X = x;
         Y = y;
      }

      public static PrimPoint FromWindowsPoint(System.Windows.Point point) => new PrimPoint(point.X, point.Y);
      public double DistTo(PrimPoint point) => Math.Sqrt((point.X - X) * (point.X - X) + (point.Y - Y) * (point.Y - Y));
      public static PrimPoint operator * (PrimPoint point, double n) => new (point.X* n, point.Y* n);
      public static PrimPoint operator + (PrimPoint point1, PrimPoint point2) => new(point1.X + point2.X, point1.Y + point2.Y);
      public static PrimPoint operator - (PrimPoint point1, PrimPoint point2) => new(point1.X - point2.X, point1.Y - point2.Y);
      public object Clone() => MemberwiseClone();
   }
}
