using System.Collections.Generic;
using System.Linq;
using GE_Primitive;

namespace GE_Model
{
   class Model
   {
      private GeoEditor.GE_ObjectsCollection _objects = new();
      private List<Layer> _layers = new();
      private int _highestObjectID = 0; //last ID
      private int _highestLayerID = 0; //last ID
      private int _currentLayerID = 0;

      public GeoEditor.GE_ObjectsCollection Objects => _objects;
      public List<Layer> Layers => _layers;
      public Layer CurrentLayer => _layers.Find(x => x.ID == _currentLayerID);

      public void DeleteLayer(int layerID)
      {
         _layers.RemoveAll(layer => layer.ID == layerID);
      }

      public int AddLayer()
      {
         _highestLayerID++;
         _layers.Add(new(_highestLayerID));
         _currentLayerID = _highestLayerID;
         return _layers.Last().ID;
      }

      public int AddSegment(PrimPoint p1, PrimPoint p2) // returns ID
      {
         _highestObjectID++;
         GE_GeomObject.Segment newSeg = new(_highestObjectID, p1, p2);
         _objects.AddObject(newSeg);
         CurrentLayer.AddObject(_highestObjectID);
         return _highestObjectID;
      }

      public void RemoveObjectWithID(int id)
      {
         _objects.RemoveObjectWithID(id);
         CurrentLayer.RemoveObjectWithId(id);
      }
   }
}