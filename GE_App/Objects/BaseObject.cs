namespace GE_GeomObject
{
   abstract class BaseObject
   {
      protected GE_Primitive.PrimPolyline _prim = new();
      public string Color { get; protected set; } = "#000000"; //hex code
      public int ID { get; protected set; }
      public double Thickness { get; protected set; } = 1;

      public void SetColor(string newColor)
      {
         Color = newColor;
      }

      public void SetThickness(double newThickness)
      {
         Thickness = newThickness;
      }

      public abstract void SetPoint(GE_Primitive.PrimPoint newPoint, int pointIndex);
      public abstract GE_Primitive.PrimPoint GetPoint(int pointIndex);
   }
}
