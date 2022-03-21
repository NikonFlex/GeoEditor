using GE_Primitive;

namespace GE_Maths
{
   class BoundRect
   {
      public PrimPoint Low { get; private set; }
      public PrimPoint High { get; private set; }
      public bool IsEmpty { get; private set; }

      public BoundRect()
      {
         Low = new(100, 100);
         High = new();
         IsEmpty = true;
      }

      public double Width => System.MathF.Abs((float)Low.X - (float)High.X);
      public double Height => System.MathF.Abs((float)Low.Y - (float)High.Y);
      public PrimPoint Center => new(Low.X + (High.X - Low.X) / 2, Low.Y + (High.Y - Low.Y) / 2);

      public void AddPoint(PrimPoint point)
      {
         if (IsEmpty)
         {
            Low = (PrimPoint)point.Clone();
            High = (PrimPoint)point.Clone();
            IsEmpty = false;
            return;
         }

         Low.X = System.MathF.Min((float)Low.X, (float)point.X);
         Low.Y = System.MathF.Min((float)Low.Y, (float)point.Y);
         High.X = System.MathF.Max((float)High.X, (float)point.X);
         High.Y = System.MathF.Max((float)High.Y, (float)point.Y);
      }
   }
}
