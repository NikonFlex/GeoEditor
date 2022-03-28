namespace GE_GeomObject
{
   abstract class BaseObject
   {
      protected GE_Primitive.PrimPolyline _prim = new();
      protected string _color = "#000000"; //hex code
      protected int _id;

      public int ID => _id;
      public string Color => _color;

      public void SetColor(string newColor)
      {
         _color = newColor;
      }

      public abstract void SetPoint(GE_Primitive.PrimPoint newPoint, int pointIndex);
      public abstract GE_Primitive.PrimPoint GetPoint(int pointIndex);
   }
}
