using GE_Primitive;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GE_ViewModel;

namespace GE_VMObject
{
   class VM_Segment : VM_BaseObject
   {
      public GE_GeomObject.Segment Segment => (GE_GeomObject.Segment)DeskViewModel.Instance.Model.Objects.ObjectsReadOnly.First(obj => obj.ID == ModelID);

      public VM_Segment(int id)
      {
         ModelID = id;
         _mainBrush = (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFrom(Segment.Color);
      }

      public override void SetPoint(PrimPoint newPoint, int pointIndex)
      {
         Segment.SetPoint(DeskViewModel.Instance.Transformator.ScreenToWorld(newPoint), pointIndex);
      }

      public override List<MovePoint> GetMovePoints()
      {
         List<MovePoint> keyPoints = new();
         List<PrimPoint> shapePoints = GetAllScreenPoints();
         for (int i = 0; i < shapePoints.Count; i++)
            keyPoints.Add(new MovePoint(shapePoints[i], this, i, MoveKind.Point));

         PrimPoint moveObjectPoint = new((shapePoints[0].X + shapePoints[1].X) / 2, (shapePoints[0].Y + shapePoints[1].Y) / 2);
         keyPoints.Add(new MovePoint(moveObjectPoint, this, shapePoints.Count, MoveKind.Object));

         return keyPoints;
      }

      public override SnapPoint GetSnapPoint(PrimPoint point)
      {
         List<SnapPoint> snapPoints = new();

         snapPoints.AddRange(trySnapToShapePoints(point));
         if (snapPoints.Count == 0)
            snapPoints.AddRange(trySnapToCenter(point));
         if (snapPoints.Count == 0)
            snapPoints.AddRange(trySnapToShapeEdge(point));

         if (snapPoints.Count == 0)
            return null;

         return findClosestSnapPoint(snapPoints, point);
      }

      public override List<PrimPoint> GetAllScreenPoints()
      {
         List<PrimPoint> points = new();
         points.Add(DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0)));
         points.Add(DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1)));
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
         PrimPoint closestPoint = GE_Maths.GE_Math.ClosestPointOnSeg(point, DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0)),
                                                                            DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1)));
         return point.DistTo(closestPoint);
      }

      public override UIElement CreateView()
      {
         return _objectUI = GeoEditor.Utils.CreateSegmentView(DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0)),
                                                              DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1)),
                                                              Segment.Thickness, _mainBrush);
      }

      public override void DeleteUI()
      {
         DeskViewModel.Instance.Screen.Children.Remove(_objectUI);
      }

      public override void RefreshUI()
      {
         System.Windows.Shapes.Line segment = _objectUI as System.Windows.Shapes.Line;
         PrimPoint p1 = DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0));
         PrimPoint p2 = DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1));
         segment.X1 = p1.X;
         segment.Y1 = p1.Y;
         segment.X2 = p2.X;
         segment.Y2 = p2.Y;
      }

      private List<SnapPoint> trySnapToShapePoints(PrimPoint point)
      {
         List<SnapPoint> snapPoints = new();

         List<PrimPoint> shapePoints = GetAllScreenPoints();
         foreach (PrimPoint shapePoint in shapePoints)
         {
            if (shapePoint.DistTo(point) <= GeoEditor.Constants.SnapDist)
               snapPoints.Add(new(shapePoint, SnapKind.Point));
         }

         return snapPoints;
      }

      private List<SnapPoint> trySnapToCenter(PrimPoint point)
      {
         List<SnapPoint> snapPoints = new();

         List<PrimPoint> shapePoints = GetAllScreenPoints();
         if (snapPoints.Count == 0)
         {
            PrimPoint centerSnapPoint = new((shapePoints[0].X + shapePoints[1].X) / 2, (shapePoints[0].Y + shapePoints[1].Y) / 2);
            if (centerSnapPoint.DistTo(point) <= GeoEditor.Constants.SnapDist)
               snapPoints.Add(new(centerSnapPoint, SnapKind.Center));
         }

         return snapPoints;
      }

      private List<SnapPoint> trySnapToShapeEdge(PrimPoint point)
      {
         List<SnapPoint> snapPoints = new();
         List<PrimPoint> shapePoints = GetAllScreenPoints();
         PrimPoint closestOnSeg = GE_Maths.GE_Math.ClosestPointOnSeg(point, shapePoints[0], shapePoints[1]);
         if (point.DistTo(closestOnSeg) <= GeoEditor.Constants.SnapDist)
            snapPoints.Add(new(closestOnSeg, SnapKind.Line));

         return snapPoints;
      }

      private SnapPoint findClosestSnapPoint(List<SnapPoint> snapPoints, PrimPoint point)
      {
         SnapPoint closestSnapPoint = snapPoints[0];

         foreach (SnapPoint snapPoint in snapPoints)
            if (snapPoint.Coord.DistTo(point) < closestSnapPoint.Coord.DistTo(point))
               closestSnapPoint = snapPoint;

         return closestSnapPoint;
      }
   }
}
