using GE_Primitive;

namespace GE_Maths
{
   static class GE_Math
   {
      public static PrimPoint ClosestPointOnSeg(PrimPoint point, PrimPoint segPoint1, PrimPoint segPoint2)
      {
         PrimVector v = new(segPoint2.X - segPoint1.X, segPoint2.Y - segPoint1.Y);
         PrimVector w = new(point.X - segPoint1.X, point.Y - segPoint1.Y);

         double s = w * v / (v * v);

         if (s < 0)
            s = 0;
         else if (s > 1)
            s = 1;

         return segPoint1 * (1 - s) + segPoint2 * s;
      }
   }
}
