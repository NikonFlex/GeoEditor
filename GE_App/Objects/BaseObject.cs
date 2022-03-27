namespace GE_GeomObject
{
   abstract class BaseObject
   {
      protected string _color = "#000000"; //hex code
      protected int _id;

      public int ID => _id;
      public string Color => _color;

      public void SetColor(string newColor)
      {
         _color = newColor;
      }
   }
}
