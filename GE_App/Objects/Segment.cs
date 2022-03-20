namespace GE_GeomObject
{
   class Segment : BaseObject
   {
      public GE_Primitive.PrimPoint P1 { get; set; }
      public GE_Primitive.PrimPoint P2 { get; set; }

      public Segment(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2)
      {
         P1 = p1;
         P2 = p2;
      }
   }
}
