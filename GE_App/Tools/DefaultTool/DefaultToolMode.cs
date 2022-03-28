using System.Windows.Input;

namespace GE_Tool
{
   abstract class DefaultToolMode
   {
      public virtual void OnMouseMove(MouseEventArgs e) { }
      public virtual void OnMouseDown(MouseButtonEventArgs e) { }
      public virtual void OnMouseUp(MouseButtonEventArgs e) { }
      public virtual void OnMouseWheel(MouseWheelEventArgs e) { }
      public virtual void OnKeyDown(KeyEventArgs e) { }
      public virtual void OnKeyUp(KeyEventArgs e) { }
   }
}
