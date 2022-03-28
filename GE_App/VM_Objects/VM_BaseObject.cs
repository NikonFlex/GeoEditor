using System.Collections.Generic;
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
      protected int _modelID;
      protected System.Windows.UIElement _objectUI;

      protected SolidColorBrush _mainBrush;
      protected SolidColorBrush _hoveredBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#01befe");
      protected SolidColorBrush _selectedBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#adff02");
      
      public int ModelID => _modelID;

      public void SetState(VMObjectState state)
      {
         System.Windows.Shapes.Shape shape = _objectUI as System.Windows.Shapes.Shape;
         switch (state)
         {
            case VMObjectState.Selected:
               shape.Effect = new System.Windows.Media.Effects.DropShadowEffect { ShadowDepth = 0, 
                                                                                  Opacity = 5, 
                                                                                  Color = _selectedBrush.Color };
               break;
            case VMObjectState.Hovered:
               shape.Effect = new System.Windows.Media.Effects.DropShadowEffect { ShadowDepth = 0, 
                                                                                  Opacity = 5, 
                                                                                  Color = _hoveredBrush.Color };
               break;
            case VMObjectState.None:
               shape.Effect = null;
               break;
         }
      }

      public abstract void SetPoint(GE_Primitive.PrimPoint newPoint, int pointIndex);
      public abstract List<GE_Primitive.PrimPoint> GetAllScreenPoints();
      public abstract List<GE_Primitive.PrimPoint> GetAllWorldPoints();
      public abstract List<KeyPoint> GetMovePoints();
      public abstract KeyPoint GetSnapPoint(GE_Primitive.PrimPoint point);
      public abstract double DistTo(GE_Primitive.PrimPoint point);
      public abstract System.Windows.UIElement CreateView();
      public abstract void DeleteUI();
      public abstract void RefreshUI();
   }
}
