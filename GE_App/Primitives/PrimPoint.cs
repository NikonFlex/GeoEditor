namespace GE_Primitive
{
   class PrimPoint : System.ICloneable
   {
      public double X { get; set; }
      public double Y { get; set; }

      public PrimPoint()
      {
         X = 0;
         Y = 0;
      }

      public PrimPoint(double x, double y)
      {
         X = x;
         Y = y;
      }

      public static PrimPoint FromWindowsPoint(System.Windows.Point point) => new PrimPoint(point.X, point.Y);

      public double DistTo(PrimPoint point)
      {
         return System.Math.Sqrt(System.Math.Pow(point.X - X, 2) + System.Math.Pow(point.Y - Y, 2));
      }

      public static PrimPoint operator * (PrimPoint point, double n)
      {
         return new(point.X * n, point.Y * n);
      }

      public static PrimPoint operator + (PrimPoint point1, PrimPoint point2)
      {
         return new(point1.X + point2.X, point1.Y + point2.Y);
      }

      public static PrimPoint operator - (PrimPoint point1, PrimPoint point2)
      {
         return new(point1.X - point2.X, point1.Y - point2.Y);
      }

      public object Clone() => MemberwiseClone();
   }
}
