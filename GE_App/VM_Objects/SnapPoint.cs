using System.Collections.Generic;
using System.Windows.Media;
using GE_Primitive;

namespace GE_VMObject
{
   enum SnapKind
   {
      Point,
      Line, 
      Center,
      Intersection
   }

   class SnapPoint
   {
      public PrimPoint Coord { get; private set; }
      public PrimRect Rect { get; private set; } = new();
      public VM_BaseObject Object { get; private set; }

      private double _radius = GeoEditor.Constants.SnapDist;
      private SnapKind _kind;
      private List<System.Windows.UIElement> _objectUI = new();
      private System.Windows.UIElement _textUI;

      public SnapPoint(PrimPoint coord, SnapKind kind)
      {
         Coord = coord;
         Rect = new(new PrimPoint(Coord.X - _radius, Coord.Y - _radius), _radius * 2, _radius * 2);
         _kind = kind;
      }

      public void Activate()
      {
         createView();
         _objectUI.ForEach(x => GE_ViewModel.DeskViewModel.Instance.Screen.Children.Add(x));
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Add(_textUI);
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
               createSnapView(GeoEditor.Constants.SnapToPointColor, "point");
               break;
            case SnapKind.Line:
               createSnapView(GeoEditor.Constants.SnapToLineColor, "line");
               break;
            case SnapKind.Center:
               createSnapView(GeoEditor.Constants.SnapToCenterColor, "center");
               break;
            case SnapKind.Intersection:
               createSnapView(GeoEditor.Constants.SnapToIntercectionColor, "intercection");
               break;
         }
      }

      private void deleteUI()
      {
         _objectUI.ForEach(x => GE_ViewModel.DeskViewModel.Instance.Screen.Children.Remove(x));
         GE_ViewModel.DeskViewModel.Instance.Screen.Children.Remove(_textUI);
      }

      private void createSnapView(SolidColorBrush brush, string text)
      {
         _objectUI.Add(GeoEditor.Utils.CreateSegmentView(new(Rect.Left, Rect.Top), new(Rect.Right, Rect.Bottom), 1, brush));
         _objectUI.Add(GeoEditor.Utils.CreateSegmentView(new(Rect.Left, Rect.Bottom), new(Rect.Right, Rect.Top), 1, brush));
         _textUI = GeoEditor.Utils.CreateTextBlock(new(Rect.Right, Rect.Top - 4 * _radius), text, GeoEditor.Constants.Black);
      }
   }
}

