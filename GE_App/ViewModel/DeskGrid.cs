using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GE_Primitive;

namespace GE_ViewModel
{
   class DeskGrid
   {
      private List<GE_GeomObject.Segment> _worldGrid = new();
      private List<System.Windows.UIElement> _gridUI = new();

      public DeskGrid()
      {
         createView();
      }

      private void createView()
      {
         PrimPoint topLeft = DeskViewModel.Instance.Transformator.ScreenToWorld(new(0, 0));
         PrimPoint bottomRight = DeskViewModel.Instance.Transformator.ScreenToWorld(new(DeskViewModel.Instance.Screen.ActualWidth,                                                                                                              DeskViewModel.Instance.Screen.ActualHeight));
         double worldScreenTop = topLeft.Y;
         double worldScreenBottom = bottomRight.Y;
         double worldScreenLeft = topLeft.X;
         double worldScreenRight = bottomRight.X;

         //x
         GE_GeomObject.Segment abscissa = new GE_GeomObject.Segment(1, new(worldScreenLeft, 0), new(worldScreenRight, 0));
         _worldGrid.Add(abscissa);
         System.Windows.UIElement abscissaUI = GeoEditor.Utils.CreateSegmentView(DeskViewModel.Instance.Transformator.WorldToScreen(abscissa.GetPoint(0)),
                                                              DeskViewModel.Instance.Transformator.WorldToScreen(abscissa.GetPoint(1)),
                                                              0.5, System.Windows.Media.Brushes.Gray);
         _gridUI.Add(abscissaUI);
         DeskViewModel.Instance.Screen.Children.Add(abscissaUI);
         //y
         GE_GeomObject.Segment ordinate = new GE_GeomObject.Segment(2, new(0, worldScreenBottom), new(0, worldScreenTop));
         _worldGrid.Add(ordinate);
         System.Windows.UIElement ordinateUI = GeoEditor.Utils.CreateSegmentView(DeskViewModel.Instance.Transformator.WorldToScreen(ordinate.GetPoint(0)),
                                                              DeskViewModel.Instance.Transformator.WorldToScreen(ordinate.GetPoint(1)),
                                                              0.5, System.Windows.Media.Brushes.Gray);
         _gridUI.Add(ordinateUI);
         DeskViewModel.Instance.Screen.Children.Add(ordinateUI);
      }

      //public void RefreshUI()
      //{
      //   PrimPoint topLeft = DeskViewModel.Instance.Transformator.ScreenToWorld(new(0, 0));
      //   PrimPoint bottomRight = DeskViewModel.Instance.Transformator.ScreenToWorld(new(DeskViewModel.Instance.Screen.ActualWidth,                                                                                                              DeskViewModel.Instance.Screen.ActualHeight));
      //   double worldScreenTop = topLeft.Y;
      //   double worldScreenBottom = bottomRight.Y;
      //   double worldScreenLeft = topLeft.X;
      //   double worldScreenRight = bottomRight.X;

      //   System.Windows.Shapes.Line segment = _objectUI as System.Windows.Shapes.Line;
      //   PrimPoint p1 = DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(0));
      //   PrimPoint p2 = DeskViewModel.Instance.Transformator.WorldToScreen(Segment.GetPoint(1));
      //   segment.X1 = p1.X;
      //   segment.Y1 = p1.Y;
      //   segment.X2 = p2.X;
      //   segment.Y2 = p2.Y;
      //}
   }
}
