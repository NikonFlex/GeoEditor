using System.Collections.Generic;
using System.Windows.Media;
using GE_Primitive;

namespace GE_VMObject
{
   enum SnapKind
   {
      Point,
      Segment
   }

   class SnapPoint
   {
      public PrimPoint Coord { get; private set; }
      public PrimRect Rect { get; private set; } = new();
      public VM_BaseObject Object { get; private set; }

      private double _radius = 11;
      private SnapKind _kind;
      private SolidColorBrush _brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#008000");
      private System.Windows.UIElement _objectUI;

      public SnapPoint(PrimPoint coord, SnapKind kind)
      {
         Coord = coord;
         Rect = new(new PrimPoint(Coord.X - _radius / 2, Coord.Y - _radius / 2), _radius, _radius);
         _kind = kind;
      }

      public void Activate()
      {
         createView();
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Add(_objectUI);
      }

      public void DeActivate()
      {
         deleteUI();
      }

      private void createView()
      {
         switch (_kind)
         {
            case SnapKind.Point:
               createPointSnapView();
               break;
            case SnapKind.Segment:
               createSegmentSnapView();
               break;
         }
      }

      private void deleteUI()
      {
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Remove(_objectUI);
      }

      private void createPointSnapView()
      {
         List<PrimPoint> pointsList = new();
         pointsList.Add(new PrimPoint(Rect.Left, Rect.Bottom));
         pointsList.Add(new PrimPoint(Rect.Left, Rect.Top));
         pointsList.Add(new PrimPoint(Rect.Right, Rect.Top));
         pointsList.Add(new PrimPoint(Rect.Right, Rect.Bottom));
         _objectUI = GeoEditor.Utils.CreatePolygon(pointsList, 1.5, _brush);
      }

      private void createSegmentSnapView()
      {
         List<PrimPoint> pointsList = new();
         pointsList.Add(new PrimPoint(Rect.Left, Rect.Bottom));
         pointsList.Add(new PrimPoint(Rect.Right, Rect.Top));
         
         pointsList.Add(new PrimPoint(Rect.Left, Rect.Top));
         pointsList.Add(new PrimPoint(Rect.Right, Rect.Bottom));
         _objectUI = GeoEditor.Utils.CreatePolyline(pointsList, 1.5, _brush);
      }
   }
}

