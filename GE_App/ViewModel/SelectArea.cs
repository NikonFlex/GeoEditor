﻿using System.Windows.Media;
using GE_Primitive;

namespace GE_ViewModel
{
   class SelectArea
   {
      private PrimRect _rect;

      public SelectArea()
      {
         _rect = new();
      }

      public SelectArea(PrimPoint p1, PrimPoint p2)
      {
         _rect = new PrimRect(p1, p1.X - p2.X, p1.Y - p2.Y);
      }

      public PrimRect Rect => _rect;

      public void SetSecondPoint(PrimPoint p2)
      {
         _rect.SetWidth(p2.X - _rect.ControlPoint.X);
         _rect.SetHeight(p2.Y - _rect.ControlPoint.Y);
      }

      public void Draw(System.Windows.Controls.Canvas screen)
      {
         //left
         screen.Children.Add(GeoEditor.Utils.createSegmentLine(new PrimPoint(_rect.Left, _rect.Top), 
                                                               new PrimPoint(_rect.Left, _rect.Bottom), Brushes.DarkOrange));
         //right
         screen.Children.Add(GeoEditor.Utils.createSegmentLine(new PrimPoint(_rect.Right, _rect.Top),
                                                               new PrimPoint(_rect.Right, _rect.Bottom), Brushes.DarkOrange));
         //top
         screen.Children.Add(GeoEditor.Utils.createSegmentLine(new PrimPoint(_rect.Left, _rect.Top),
                                                               new PrimPoint(_rect.Right, _rect.Top), Brushes.DarkOrange));
         //bottom
         screen.Children.Add(GeoEditor.Utils.createSegmentLine(new PrimPoint(_rect.Left, _rect.Bottom),
                                                               new PrimPoint(_rect.Right, _rect.Bottom), Brushes.DarkOrange));
      }

      private bool isPointInside(PrimPoint point)
      {
         return _rect.Left <= point.X && point.X <= _rect.Right && _rect.Top <= point.Y && point.Y <= _rect.Bottom;
      }

      public bool IsSegmentInside(GE_GeomObject.Segment segment)
      {
         return isPointInside(DeskViewModel.Instance.Transformator.WorldToScreen(segment.P1)) && 
                isPointInside(DeskViewModel.Instance.Transformator.WorldToScreen(segment.P2));
      }
   }
}