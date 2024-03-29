﻿using System.Collections.Generic;
using System.Windows.Media;

namespace GE_VMObject
{
   enum VMObjectState
   {
      None,
      Hovered,
      Selected
   }

   abstract class VM_BaseObject
   {
      protected System.Windows.UIElement _objectUI;
      protected SolidColorBrush _mainBrush;

      public bool IsVisible { get; protected set; } = true;
      public int ModelID { get; protected set; }
      
      public void SetState(VMObjectState state)
      {
         System.Windows.Shapes.Shape shape = _objectUI as System.Windows.Shapes.Shape;
         switch (state)
         {
            case VMObjectState.Selected:
               shape.Effect = new System.Windows.Media.Effects.DropShadowEffect { ShadowDepth = 0, 
                                                                                  Opacity = 5, 
                                                                                  Color = GeoEditor.Constants.SelectedColor.Color };
               break;
            case VMObjectState.Hovered:
               shape.Effect = new System.Windows.Media.Effects.DropShadowEffect { ShadowDepth = 0, 
                                                                                  Opacity = 5, 
                                                                                  Color = GeoEditor.Constants.HoveredColor.Color };
               break;
            case VMObjectState.None:
               shape.Effect = null;
               break;
         }
      }

      public void SetVisible(bool isVisible)
      {
         IsVisible = isVisible;
         if (isVisible)
            _objectUI.Visibility = System.Windows.Visibility.Visible;
         else
            _objectUI.Visibility = System.Windows.Visibility.Hidden;
      }

      public abstract void SetPoint(GE_Primitive.PrimPoint newPoint, int pointIndex);
      public abstract List<GE_Primitive.PrimPoint> GetAllScreenPoints();
      public abstract List<GE_Primitive.PrimPoint> GetAllWorldPoints();
      public abstract List<MovePoint> GetMovePoints();
      public abstract SnapPoint GetSnapPoint(GE_Primitive.PrimPoint point);
      public abstract double DistTo(GE_Primitive.PrimPoint point);
      public abstract System.Windows.UIElement CreateView();
      public abstract void DeleteUI();
      public abstract void RefreshUI();
   }
}
