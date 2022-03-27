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
      protected SolidColorBrush _hoveredBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#fca311");
      protected SolidColorBrush _selectedBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#e63946");
      
      public int ModelID => _modelID;

      public void SetState(VMObjectState state)
      {
         System.Windows.Shapes.Shape shape = _objectUI as System.Windows.Shapes.Shape;
         switch (state)
         {
            case VMObjectState.Selected:
               shape.Stroke = _selectedBrush;
               break;
            case VMObjectState.Hovered:
               shape.Stroke = _hoveredBrush;
               break;
            case VMObjectState.None:
               shape.Stroke = _mainBrush;
               break;
         }
      }

      public abstract List<GE_Primitive.PrimPoint> GetAllScreenPoints();
      public abstract List<GE_Primitive.PrimPoint> GetAllWorldPoints();
      public abstract List<GE_Primitive.PrimPoint> GetMovePoints();
      public abstract List<GE_Primitive.PrimPoint> GetSnapPoints();
      public abstract double DistTo(GE_Primitive.PrimPoint point);
      public abstract System.Windows.UIElement CreateView();
      public abstract void DeleteUI();
      public abstract void RefreshUI();
   }
}
