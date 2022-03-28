using System.Collections.Generic;
using System.Linq;

namespace GE_ViewModel
{ 
   class DeskViewModel
   {
      public static DeskViewModel Instance { get; } = new();
      public GE_Maths.Transformator Transformator { get; private set; }
      public GeoEditor.GE_VM_ObjectsCollection ObjectsViews { get; private set; } = new();
      public GeoEditor.GE_SelectionCollection SelectedObjects { get; private set; } = new();
      public System.Windows.Controls.Canvas Screen { get; private set; }
      public SelectArea SelectArea { get; private set; } = new();
      public Phantom Phantom { get; private set; } = new();

      private DeskViewModel() { }

      public void SetScreen(System.Windows.Controls.Canvas screen)
      {
         Screen = screen;
         Transformator = new(Screen.ActualWidth, Screen.ActualHeight);
         Screen.Children.Add(Phantom.CreateView());
         Screen.Children.Add(SelectArea.CreateView());
      }

      public void AddSegment(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2)
      {
         int objID = GE_Model.Model.Instance.AddSegment(Transformator.ScreenToWorld(p1), Transformator.ScreenToWorld(p2));
         ObjectsViews.AddObject(new GE_VMObject.VM_Segment(objID));
         Screen.Children.Add(ObjectsViews.ObjectsReadOnly.Last().CreateView());
      }

      public void AddKeyPoint(GE_VMObject.KeyPoint keyPoint)
      {
         Screen.Children.Add(keyPoint.CreateView());
      }

      public void RefreshView()
      {
         foreach (GE_VMObject.VM_BaseObject view in ObjectsViews.ObjectsReadOnly)
            view.RefreshUI();
      }
   }
}
