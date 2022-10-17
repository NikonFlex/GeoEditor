using System.Windows.Input;

namespace GE_Tool
{
   abstract class DefaultToolMode
   {
      protected bool _isCtrlPressed;
      public virtual void OnMouseMove(MouseEventArgs e) { }
      public virtual void OnMouseDown(MouseButtonEventArgs e) { }
      public virtual void OnMouseUp(MouseButtonEventArgs e) { }
      public virtual void OnMouseWheel(MouseWheelEventArgs e) { }
      public void OnKeyDown(KeyEventArgs e) => _isCtrlPressed = e.Key == Key.LeftCtrl;
      public void OnKeyUp(KeyEventArgs e) => _isCtrlPressed = !(e.Key == Key.LeftCtrl);
   }
}
