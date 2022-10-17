namespace GE_GeomObject
{
   class Segment : BaseObject
   {
      public Segment(int id, GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2)
      {
         _prim.Points.Add(p1);
         _prim.Points.Add(p2);
         ID = id;
      }

      public override void SetPoint(GE_Primitive.PrimPoint newPoint, int pointIndex) => _prim.Points[pointIndex] = newPoint;
      public override GE_Primitive.PrimPoint GetPoint(int pointIndex) => _prim.Points[pointIndex];
   }
}
