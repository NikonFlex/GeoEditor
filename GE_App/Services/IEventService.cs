using System.Windows.Input;

namespace GE_Service
{
   interface IEventService
   {
      public delegate void MouseMoveHandler(MouseEventArgs e);
      public event MouseMoveHandler OnMouseMove;

      public delegate void MouseDownHandler(MouseButtonEventArgs e);
      public event MouseDownHandler OnMouseDown;

      public delegate void MouseUpHandler(MouseButtonEventArgs e);
      public event MouseUpHandler OnMouseUp;

      public delegate void MouseWheelHandler(MouseWheelEventArgs e);
      public event MouseWheelHandler OnMouseWheel;

      public delegate void KeyDownHandler(KeyEventArgs e);
      public event KeyDownHandler OnKeyDown;

      public delegate void KeyUpHandler(KeyEventArgs e);
      public event KeyUpHandler OnKeyUp;
   }
}
