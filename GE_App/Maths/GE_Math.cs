namespace GE_Maths
{
   static class GE_Math
   {
      public static double PointToSegmentDist(GE_Primitive.PrimPoint point, GE_Primitive.PrimPoint segPoint1, GE_Primitive.PrimPoint segPoint2)
      {
         GE_Primitive.PrimVector v = new(segPoint2.X - segPoint1.X, segPoint2.Y - segPoint1.Y);
         GE_Primitive.PrimVector w = new(point.X - segPoint1.X, point.Y - segPoint1.Y);

         double s = (w * v) / (v * v);

         if (s < 0)
            s = 0;
         else if (s > 1)
            s = 1;

         GE_Primitive.PrimPoint closestPoint = segPoint1 * (1 - s) + segPoint2 * s;
         return point.DistTo(closestPoint);
      }
   }
}
