using GE_Primitive;
using System.Collections.Generic;
using System.Windows.Media;

namespace GE_VMObject
{
   enum MoveKind
   {
      Point,
      Object
   }

   class MovePoint
   {
      public PrimPoint Coord { get; private set; }
      public VM_BaseObject Object { get; private set; }
      public int PointIndex { get; private set; }
      public bool IsActive { get; private set; }
      public double Radius { get; private set; } = 3.5;
      public MoveKind _kind { get; private set; }

      private System.Windows.UIElement _objectUI;

      public MovePoint(PrimPoint point, VM_BaseObject obj, int pointIndex, MoveKind kind) //если двигается весь объект, то вместо индекса колво точек
      {
         Coord = point;
         Object = obj;
         PointIndex = pointIndex;
         _kind = kind;
      }

      private SolidColorBrush _mainBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#8f00ff");
     
      public void SetState(VMObjectState state)
      {
         System.Windows.Shapes.Shape shape = _objectUI as System.Windows.Shapes.Shape;
         switch (state)
         {
            case VMObjectState.Selected:
               shape.Fill = GeoEditor.Constants.SelectedColor;
               IsActive = true;
               break;
            case VMObjectState.Hovered:
               shape.Fill = GeoEditor.Constants.HoveredColor;
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
         switch (_kind)
         {
            case MoveKind.Point:
               Object.SetPoint(newPoint, PointIndex);
               break;
            case MoveKind.Object:
               moveObject(newPoint);
               break;
         }
         Coord = newPoint;
         Object.RefreshUI();
         RefreshUI();
      }

      public System.Windows.UIElement CreateView()
      {
         var pointsList = GeoEditor.Utils.CreateRegularFigure(Coord, Radius, 36);
         return _objectUI = GeoEditor.Utils.CreatePolygon(pointsList, 0.9, GeoEditor.Constants.Black, _mainBrush);
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

      private void moveObject(PrimPoint newPoint)
      {
         List<PrimPoint> shapePoints = Object.GetAllScreenPoints();
         for (int i = 0; i < PointIndex; i++)
            Object.SetPoint(shapePoints[i] + (newPoint - Coord), i);
      }
   }
}
