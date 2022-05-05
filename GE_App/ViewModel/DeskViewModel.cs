using System.Collections.Generic;
using System.Linq;

namespace GE_ViewModel
{ 
   class DeskViewModel
   {
      public static DeskViewModel Instance { get; } = new();
      public GE_Model.Model Model { get; private set; } = new();
      public GE_Maths.Transformator Transformator { get; private set; }
      public GeoEditor.GE_VM_ObjectsCollection ObjectsViews { get; private set; } = new();
      public GeoEditor.GE_SelectionCollection SelectedObjects { get; private set; } = new();
      public System.Windows.Controls.Canvas Screen { get; private set; }
      public SelectArea SelectArea { get; private set; } = new();
      public Phantom Phantom { get; private set; } = new();
      public DeskGrid Grid { get; private set; }

      private DeskViewModel() { }

      public void SetScreen(System.Windows.Controls.Canvas screen)
      {
         Screen = screen;
         Transformator = new(Screen.ActualWidth, Screen.ActualHeight);
         Screen.Children.Add(Phantom.CreateView());
         Screen.Children.Add(SelectArea.CreateView());
      }

      public void ShowObject(int id)
      {

      }

      public void HideObject(int id)
      {

      }

      public void AddSegment(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2)
      {
         int objID = Model.AddSegment(Transformator.ScreenToWorld(p1), Transformator.ScreenToWorld(p2));
         ObjectsViews.AddObject(new GE_VMObject.VM_Segment(objID));
         Screen.Children.Add(ObjectsViews.ObjectsReadOnly.Last().CreateView());
      }

      public GE_VMObject.SnapPoint GetSnapPoint(GE_Primitive.PrimPoint mousePos, GE_VMObject.VM_BaseObject moveObject = null)
      {
         GE_VMObject.SnapPoint currentSnapPoint = null;
         foreach (GE_VMObject.VM_BaseObject obj in ObjectsViews.ObjectsReadOnly)
         {
            if (obj == moveObject)
               continue;

            GE_VMObject.SnapPoint snapPoint = obj.GetSnapPoint(mousePos);

            if (snapPoint is null)
               continue;
            else if (currentSnapPoint is null || snapPoint.Coord.DistTo(mousePos) <= currentSnapPoint.Coord.DistTo(mousePos))
               currentSnapPoint = snapPoint;
         }
         return currentSnapPoint;
      }

      public void RefreshView()
      {
         foreach (GE_VMObject.VM_BaseObject view in ObjectsViews.ObjectsReadOnly)
            view.RefreshUI();
      }
   }
}
