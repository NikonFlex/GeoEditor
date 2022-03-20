using GE_Primitive;

namespace GE_Model
{
   class Model
   {
      private static Model _instance = new();
      private static GeoEditor.GE_ObjectsCollection _objects = new();

      private Model() { }

      public static Model Instance => _instance;
      public GeoEditor.GE_ObjectsCollection Objects => _objects;

      public void AddSegment(PrimPoint p1, PrimPoint p2)
      {
         _objects.AddObject(new GE_GeomObject.Segment(p1, p2));
      }

      public void RemoveObjectAt(int index)
      {
         _objects.RemoveObject(index);
      }
   }
}