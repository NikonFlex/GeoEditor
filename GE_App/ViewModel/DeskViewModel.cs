using System.Collections.Generic;

namespace GE_ViewModel
{
   class DeskViewModel
   {
      private static DeskViewModel _instance = new();
      private static GE_Maths.Transformator _transformator;
      private System.Windows.Controls.Canvas _screen;

      private List<int> _selectedObjectsI = new();
      
      private Phantom _phantom = new();
      private SelectArea _selectArea = new();
      private int _phantomIOnScene;
      private int _selectAreaIOnScene;
      
      private DeskViewModel() { }

      public static DeskViewModel Instance => _instance;
      public System.Windows.Controls.Canvas Screen => _screen;
      public GE_Maths.Transformator Transformator => _transformator;
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
         int counter = 0;
         foreach (GE_GeomObject.BaseObject obj in GE_Model.Model.Instance.Objects.ObjectsReadOnly)
         {
            if (obj is GE_GeomObject.Segment seg)
               _screen.Children.Add(getView(seg, _selectedObjectsI.Contains(counter)));
            counter++;
         }
      }

      public void SelectObjectAt(int index)
      {
         if (_selectedObjectsI.Contains(index) == false)
            _selectedObjectsI.Add(index);
      }

      public void DeSelectObjectAt(int index)
      {
         _selectedObjectsI.Remove(index);
      }

      public bool IsObjectSelectedAt(int index)
      {
         return _selectedObjectsI.Contains(index);
      }

      private static System.Windows.Shapes.Line getView(GE_GeomObject.Segment segment, bool selected)
      {
         return GeoEditor.Utils.createSegmentLine(_transformator.WorldToScreen(segment.P1),
                                                  _transformator.WorldToScreen(segment.P2),
                                                  selected ? System.Windows.Media.Brushes.DarkViolet : System.Windows.Media.Brushes.Black);
      }

      private void drawCenter()
      {
         GE_Primitive.PrimPoint worldCenter = _transformator.WorldToScreen(new GE_Primitive.PrimPoint(0, 0));
         _screen.Children.Add(GeoEditor.Utils.createSegmentLine(new(worldCenter.X - 5, worldCenter.Y), 
                                                                new(worldCenter.X + 5, worldCenter.Y), System.Windows.Media.Brushes.Black));

         _screen.Children.Add(GeoEditor.Utils.createSegmentLine(new(worldCenter.X, worldCenter.Y - 5),
                                                                new(worldCenter.X, worldCenter.Y + 5), System.Windows.Media.Brushes.Black));

      }
   }
}
