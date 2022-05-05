using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GE_Model
{
   class Layer
   {
      public int ID { get; private set; }
      public bool IsVisible { get; private set; } = true;

      private List<int> _objects = new();

      public IReadOnlyCollection<int> Objects => _objects;

      public Layer(int index)
      {
         ID = index;
      }

      public void SetVisible(bool isVisible)
      {
         IsVisible = isVisible;
         foreach (int objectID in _objects)
            GE_ViewModel.DeskViewModel.Instance.ObjectsViews.FindObject(objectID).SetVisible(isVisible);
      }

      public void AddObject(int id)
      {
         _objects.Add(id);
      }

      public void RemoveObjectWithId(int id)
      {
         _objects.Remove(id);
      }
   }
}