using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using GE_Primitive;

namespace GE_ViewModel
{
   class SelectArea
   {
      private PrimRect _rect;
      private System.Windows.UIElement _selectAreaUI;

      public SelectArea()
      {
         _rect = new();
      }

      public SelectArea(PrimPoint p1, PrimPoint p2)
      {
         _rect = new(p1, p1.X - p2.X, p1.Y - p2.Y);
      }

      public PrimRect Rect => _rect;

      public void SetControlPoint(PrimPoint p1)
      {
         _rect = new(p1, 0, 0);
         updateSelectAreaUI();
      }

      public void SetSecondPoint(PrimPoint p2)
      {
         _rect.SetWidth(p2.X - _rect.ControlPoint.X);
         _rect.SetHeight(p2.Y - _rect.ControlPoint.Y);
         updateSelectAreaUI();
      }

      public void Clear()
      {
         _rect = new();
         updateSelectAreaUI();
      }

      public System.Windows.UIElement CreateView()
      {
         List<PrimPoint> pointsList = new();
         pointsList.Add(new PrimPoint(_rect.Left, _rect.Bottom));
         pointsList.Add(new PrimPoint(_rect.Left, _rect.Top));
         pointsList.Add(new PrimPoint(_rect.Right, _rect.Top));
         pointsList.Add(new PrimPoint(_rect.Right, _rect.Bottom));
         return _selectAreaUI = GeoEditor.Utils.CreatePolygon(pointsList, 1, Brushes.RoyalBlue);
      }

      public bool IsInside(List<PrimPoint> points)
      {
         return points.All(point => isPointInside(point));
      }

      private void updateSelectAreaUI()
      {
         System.Windows.Shapes.Polygon polygon = _selectAreaUI as System.Windows.Shapes.Polygon;
         polygon.Points.Clear();
         polygon.Points.Add(new System.Windows.Point(_rect.Left, _rect.Bottom));
         polygon.Points.Add(new System.Windows.Point(_rect.Left, _rect.Top));
         polygon.Points.Add(new System.Windows.Point(_rect.Right, _rect.Top));
         polygon.Points.Add(new System.Windows.Point(_rect.Right, _rect.Bottom));
      }

      private bool isPointInside(PrimPoint point)
      {
         return _rect.Left <= point.X && point.X <= _rect.Right && _rect.Top <= point.Y && point.Y <= _rect.Bottom;
      }
   }
}
