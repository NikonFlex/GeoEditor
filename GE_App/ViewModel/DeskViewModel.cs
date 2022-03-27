using System.Collections.Generic;
using System.Linq;

namespace GE_ViewModel
{ 
   class DeskViewModel
   {
      private static DeskViewModel _instance = new();
      private static GE_Maths.Transformator _transformator;
      private GeoEditor.GE_VM_ObjectsCollection _objectsViews = new();
      private GeoEditor.GE_SelectionCollection _selectedObjects = new();
      private System.Windows.Controls.Canvas _screen;
      
      private SelectArea _selectArea = new();
      private Phantom _phantom = new();
      
      private DeskViewModel() { }

      public static DeskViewModel Instance => _instance;
      public System.Windows.Controls.Canvas Screen => _screen;
      public GE_Maths.Transformator Transformator => _transformator;
      public GeoEditor.GE_VM_ObjectsCollection ObjectsViews => _objectsViews;
      public GeoEditor.GE_SelectionCollection SelectedObjects => _selectedObjects;
      public SelectArea SelectArea => _selectArea;
      public Phantom Phantom => _phantom;

      public void SetScreen(System.Windows.Controls.Canvas screen)
      {
         _screen = screen;
         _transformator = new(_screen.ActualWidth, _screen.ActualHeight);
         _screen.Children.Add(_phantom.CreateView());
         _screen.Children.Add(_selectArea.CreateView());
         //drawCenter();
      }

      public void SetPhantomGeometry(GE_Primitive.PrimPolyline newPhantom)
      {
         _phantom.SetGeometry(newPhantom);
      }

      public void AddSegment(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2)
      {
         int objID = GE_Model.Model.Instance.AddSegment(_transformator.ScreenToWorld(p1), _transformator.ScreenToWorld(p2));
         _objectsViews.AddObject(new GE_VMObject.VM_Segment(objID));
         _screen.Children.Add(_objectsViews.ObjectsReadOnly.Last().CreateView());
      }

      public void RefreshView()
      {
         foreach (GE_VMObject.VM_BaseObject view in _objectsViews.ObjectsReadOnly)
            view.RefreshUI();
      }

      //public void MakeNewView()
      //{
      //   foreach (GE_ view in _objectsViews.ObjectsReadOnly)
      //      view.RefreshUI();
      //}

      //private void drawCenter()
      //{
      //   GE_Primitive.PrimPoint worldCenter = _transformator.WorldToScreen(new GE_Primitive.PrimPoint(0, 0));
      //   _screen.Children.Add(GeoEditor.Utils.CreateSegmentView(new(worldCenter.X - 5, worldCenter.Y), 
      //                                                          new(worldCenter.X + 5, worldCenter.Y), 1, System.Windows.Media.Brushes.Black));

      //   _screen.Children.Add(GeoEditor.Utils.CreateSegmentView(new(worldCenter.X, worldCenter.Y - 5),
      //                                                          new(worldCenter.X, worldCenter.Y + 5), 1, System.Windows.Media.Brushes.Black));

      //}
   }
}
