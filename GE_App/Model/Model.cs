using GE_Primitive;

namespace GE_Model
{
   class Model
   {
      private static Model _instance = new();
      private static GeoEditor.GE_ObjectsCollection _objects = new();

      private int _highestID = 0; //last ID

      private Model() { }

      public static Model Instance => _instance;
      public GeoEditor.GE_ObjectsCollection Objects => _objects;

      public int AddSegment(PrimPoint p1, PrimPoint p2) // returns ID
      {
         _highestID++;
         GE_GeomObject.Segment newSeg = new(_highestID, p1, p2);
         _objects.AddObject(newSeg);
         return _highestID;
      }

      public void RemoveObjectWithID(int id)
      {
         _objects.RemoveObjectWithID(id);
      }
   }
}