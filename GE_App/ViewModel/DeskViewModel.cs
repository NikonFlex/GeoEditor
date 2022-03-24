using System.Collections.Generic;
using System.Linq;

namespace GE_ViewModel
{
   class DeskViewModel
   {
      private static DeskViewModel _instance = new();
      private static GE_Maths.Transformator _transformator;
      private System.Windows.Controls.Canvas _screen;
      private GeoEditor.GE_VM_ObjectsCollection _objectsViews = new();
      
      private Phantom _phantom = new();
      private SelectArea _selectArea = new();
      private int _phantomIOnScene;
      private int _selectAreaIOnScene;
      
      private DeskViewModel() { }

      public static DeskViewModel Instance => _instance;
      public System.Windows.Controls.Canvas Screen => _screen;
      public GE_Maths.Transformator Transformator => _transformator;
      public GeoEditor.GE_VM_ObjectsCollection ObjectsViews => _objectsViews;
      public SelectArea SelectArea
      {
         get => _selectArea;
         set => _selectArea = value;
      }

      public void SetScreen(System.Windows.Controls.Canvas screen)
      {
         _screen = screen;
         _transformator = new(_screen.ActualWidth, _screen.ActualHeight);
         RefreshView();
      }

      public void SetPhantomGeometry(GE_Primitive.PrimPolyline newPhantom)
      {
         _phantom.Primitive = newPhantom;
      }

      public void RefreshPhantom(bool firstSet = false)
      {
         if (!firstSet)
            _screen.Children.RemoveAt(_phantomIOnScene);
         _phantom.Draw(_screen);
         _phantomIOnScene = _screen.Children.Count - 1;
      }

      public void SetSelectAreaGeometry(GE_Primitive.PrimPoint topLeft, GE_Primitive.PrimPoint bottomRight)
      {
         _selectArea = new SelectArea(topLeft, bottomRight);
      }

      public void RefreshSelectArea(bool firstSet = false)
      {
         if (!firstSet)
            for (int i = 0; i < 4; i++)
               _screen.Children.RemoveAt(_selectAreaIOnScene);
         _selectArea.Draw(_screen);
         _selectAreaIOnScene = _screen.Children.Count - 4;
      }

      public void RefreshView()
      {
         _screen.Children.Clear();

         drawCenter();
         foreach (GE_VM_Object.VM_BaseObject view in _objectsViews.ObjectsReadOnly)
            view.Draw(_screen);
      }

      public void AddSegment(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2)
      {
         int objID = GE_Model.Model.Instance.AddSegment(_transformator.ScreenToWorld(p1), _transformator.ScreenToWorld(p2));
         _objectsViews.AddObject(new GE_VM_Object.VM_Segment(objID));
         RefreshView();
      }

      private void drawCenter()
      {
         GE_Primitive.PrimPoint worldCenter = _transformator.WorldToScreen(new GE_Primitive.PrimPoint(0, 0));
         _screen.Children.Add(GeoEditor.Utils.createSegmentLine(new(worldCenter.X - 5, worldCenter.Y), 
                                                                new(worldCenter.X + 5, worldCenter.Y), 1, System.Windows.Media.Brushes.Black));

         _screen.Children.Add(GeoEditor.Utils.createSegmentLine(new(worldCenter.X, worldCenter.Y - 5),
                                                                new(worldCenter.X, worldCenter.Y + 5), 1, System.Windows.Media.Brushes.Black));

      }
   }
}
