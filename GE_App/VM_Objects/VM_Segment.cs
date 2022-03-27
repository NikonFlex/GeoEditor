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

      public VM_Segment(int id)
      {
         _modelID = id;
         _mainBrush = (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFrom(Segment.Color);
      }

      public override List<PrimPoint> GetMovePoints()
      {
         throw new System.NotImplementedException();
      }

      public override List<PrimPoint> GetSnapPoints()
      {
         throw new System.NotImplementedException();
      }
      public override List<PrimPoint> GetAllScreenPoints()
      {
         List<PrimPoint> points = new();
         points.Add(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P1));
         points.Add(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P2));
         return points;
      }

      public override List<PrimPoint> GetAllWorldPoints()
      {
         List<PrimPoint> points = new();
         points.Add(Segment.P1);
         points.Add(Segment.P2);
         return points;
      }

      public override double DistTo(PrimPoint point)
      {
         return GE_Maths.GE_Math.PointToSegmentDist(point, GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P1),
                                                           GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P2));
      }

      public override UIElement CreateView()
      {
         return _objectUI = GeoEditor.Utils.CreateSegmentView(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P1),
                                                  GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P2),
                                                  1.5, _mainBrush);
      }

      public override void DeleteUI()
      {
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Remove(_objectUI);
      }

      public override void RefreshUI()
      {
         System.Windows.Shapes.Line segment = _objectUI as System.Windows.Shapes.Line;
         PrimPoint p1 = GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P1);
         PrimPoint p2 = GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P2);
         segment.X1 = p1.X;
         segment.Y1 = p1.Y;
         segment.X2 = p2.X;
         segment.Y2 = p2.Y;
      }
   }
}
