using GE_Primitive;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GE_VMObject
{
   class VM_Segment : VM_BaseObject
   {
      public GE_GeomObject.Segment Segment => (GE_GeomObject.Segment)GE_Model.Model.Instance.Objects.ObjectsReadOnly.First(obj => obj.ID == _modelID);

      private double _eps = 3;

      public VM_Segment(int id)
      {
         _modelID = id;
         _mainBrush = (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFrom(Segment.Color);
      }

      public override void SetPoint(PrimPoint newPoint, int pointIndex)
      {
         Segment.SetPoint(GE_ViewModel.DeskViewModel.Instance.Transformator.ScreenToWorld(newPoint), pointIndex);
      }

      public override List<KeyPoint> GetMovePoints()
      {
         List<KeyPoint> keyPoints = new();
         List<PrimPoint> shapePoints = GetAllScreenPoints();
         for (int i = 0; i < shapePoints.Count; i++)
            keyPoints.Add(new KeyPoint(shapePoints[i], this, i));

         return keyPoints;
      }

      public override KeyPoint GetSnapPoint(PrimPoint point)
      {
         List<KeyPoint> snapPoints = new();

         List<PrimPoint> shapePoints = GetAllScreenPoints();
         foreach (PrimPoint shapePoint in shapePoints)
         {
            if (shapePoint.DistTo(point) <= _eps)
               snapPoints.Add(new(shapePoint, this, -1));
         }

         if (snapPoints.Count == 0)
            return null;

         KeyPoint closestSnapPoint = snapPoints[0];

         foreach (KeyPoint snapPoint in snapPoints)
            if (snapPoint.Point.DistTo(point) < closestSnapPoint.Point.DistTo(point))
               closestSnapPoint = snapPoint;

         return closestSnapPoint;
      }

      public override List<PrimPoint> GetAllScreenPoints()
      {
         List<PrimPoint> points = new();
         points.Add(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0)));
         points.Add(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1)));
         return points;
      }

      public override List<PrimPoint> GetAllWorldPoints()
      {
         List<PrimPoint> points = new();
         points.Add(Segment.GetPoint(0));
         points.Add(Segment.GetPoint(1));
         return points;
      }

      public override double DistTo(PrimPoint point)
      {
         PrimPoint closestPoint = GE_Maths.GE_Math.ClosestPointOnSeg(point, GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0)),
                                                                            GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1)));
         return point.DistTo(closestPoint);
      }

      public override UIElement CreateView()
      {
         return _objectUI = GeoEditor.Utils.CreateSegmentView(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0)),
                                                              GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1)),
                                                              1.5, _mainBrush);
      }

      public override void DeleteUI()
      {
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Remove(_objectUI);
      }

      public override void RefreshUI()
      {
         System.Windows.Shapes.Line segment = _objectUI as System.Windows.Shapes.Line;
         PrimPoint p1 = GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0));
         PrimPoint p2 = GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1));
         segment.X1 = p1.X;
         segment.Y1 = p1.Y;
         segment.X2 = p2.X;
         segment.Y2 = p2.Y;
      }
   }
}
