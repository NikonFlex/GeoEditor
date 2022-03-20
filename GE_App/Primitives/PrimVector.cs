namespace GE_Primitive
{
   class PrimVector
   {
      public PrimPoint P1 { get; set; }
      public PrimPoint P2 { get; set; }

      public double X { get; private set; }
      public double Y { get; private set; }

      public PrimVector()
      {
         P1 = new();
         P2 = new();
         X = 0;
         Y = 0;
      }

      public PrimVector(PrimPoint p1, PrimPoint p2)
      {
         P1 = p1;
         P2 = p2;
         X = p2.X - p1.X;
         Y = p2.Y - p1.Y;
      }

      public PrimVector(double x, double y)
      {
         X = x;
         Y = y;
         P1 = new();
         P2 = new();
      }

      public static double operator * (PrimVector vector1, PrimVector vector2)
      {
         return vector1.X * vector2.X + vector1.Y * vector2.Y;
      }
   }
}
