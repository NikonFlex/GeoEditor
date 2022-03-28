using GE_Primitive;
using System.Collections.Generic;
using System.Windows.Media;

namespace GE_VMObject
{
   class KeyPoint
   {
      public PrimPoint Coord { get; private set; }
      public VM_BaseObject Object { get; private set; }
      public int PointIndex { get; private set; }
      public bool IsActive { get; private set; }
      public double Radius { get; private set; } = 3.5;

      private System.Windows.UIElement _objectUI;

      public KeyPoint(PrimPoint point, VM_BaseObject obj, int pointIndex)
      {
         Coord = point;
         Object = obj;
         PointIndex = pointIndex;
      }

      protected SolidColorBrush _mainBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#8f00ff");
      protected SolidColorBrush _hoveredBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#01befe");
      protected SolidColorBrush _selectedBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#adff02");

      public void SetState(VMObjectState state)
      {
         System.Windows.Shapes.Shape shape = _objectUI as System.Windows.Shapes.Shape;
         switch (state)
         {
            case VMObjectState.Selected:
               shape.Fill = _selectedBrush;
               IsActive = true;
               break;
            case VMObjectState.Hovered:
               shape.Fill = _hoveredBrush;
               IsActive = false;
               break;
            case VMObjectState.None:
               shape.Fill = _mainBrush;
               IsActive = false;
               break;
         }
      }

      public void SetPoint(PrimPoint newPoint)
      {
         Coord = newPoint;
         Object.SetPoint(newPoint, PointIndex);
         Object.RefreshUI();
         RefreshUI();
      }

      public System.Windows.UIElement CreateView()
      {
         var pointsList = GeoEditor.Utils.CreateRegularFigure(Coord, Radius, 36); // hexagon
         return _objectUI = GeoEditor.Utils.CreatePolygon(pointsList, 0.9, (SolidColorBrush)new BrushConverter().ConvertFrom("#000000"), _mainBrush);
      }

      public void DeleteUI()
      {
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Remove(_objectUI);
      }

      public void RefreshUI()
      {
         List<PrimPoint> points = GeoEditor.Utils.CreateRegularFigure(Coord, Radius, 45);

         System.Windows.Shapes.Polygon polygon = _objectUI as System.Windows.Shapes.Polygon;
         polygon.Points.Clear();
         foreach (PrimPoint point in points)
            polygon.Points.Add(new System.Windows.Point(point.X, point.Y));

      }
   }
}
