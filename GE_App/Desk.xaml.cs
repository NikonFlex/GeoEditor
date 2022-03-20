using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GE_Service;

namespace GeoEditor.Control
{
   public partial class Desk : UserControl, IEventService
   {
      private event IEventService.MouseMoveHandler _onMouseMove;
      private event IEventService.MouseDownHandler _onMouseDown;
      private event IEventService.MouseUpHandler _onMouseUp;
      private event IEventService.MouseWheelHandler _onMouseWheel;
      private event IEventService.KeyDownHandler _onKeyDown;
      private event IEventService.KeyUpHandler _onKeyUp;

      public Desk()
      {
         InitializeComponent();
         Loaded += deskLoaded;
         ServicesContainer.Register<IEventService>(this);
      }

      private void deskLoaded(object sender, RoutedEventArgs e)
      {
         Window window = Window.GetWindow(this);
         window.KeyDown += keyDownEvent;
         window.KeyUp += keyUpEvent;
      }

      event IEventService.MouseMoveHandler IEventService.OnMouseMove
      {
         add => _onMouseMove += value;
         remove => _onMouseMove -= value;
      }

      event IEventService.MouseDownHandler IEventService.OnMouseDown
      {
         add => _onMouseDown += value;
         remove => _onMouseDown -= value;
      }

      event IEventService.MouseUpHandler IEventService.OnMouseUp
      {
         add => _onMouseUp += value;
         remove => _onMouseUp -= value;
      }

      event IEventService.MouseWheelHandler IEventService.OnMouseWheel
      {
         add => _onMouseWheel += value;
         remove => _onMouseWheel -= value;
      }

      event IEventService.KeyDownHandler IEventService.OnKeyDown
      {
         add => _onKeyDown += value;
         remove => _onKeyDown -= value;
      }

      event IEventService.KeyUpHandler IEventService.OnKeyUp
      {
         add => _onKeyUp += value;
         remove => _onKeyUp -= value;
      }

      private void mouseMoveEvent(object sender, MouseEventArgs e)
      {
         _onMouseMove?.Invoke(e);
      }

      private void mouseDownEvent(object sender, MouseButtonEventArgs e)
      {
         _onMouseDown?.Invoke(e);
      }

      private void mouseUpEvent(object sender, MouseButtonEventArgs e)
      {
         _onMouseUp?.Invoke(e);
      }

      private void mouseWheelEvent(object sender, MouseWheelEventArgs e)
      {
         _onMouseWheel?.Invoke(e);
      }

      private void keyDownEvent(object sender, KeyEventArgs e)
      {
         _onKeyDown?.Invoke(e);
      }

      private void keyUpEvent(object sender, KeyEventArgs e)
      {
         _onKeyUp?.Invoke(e);
      }

   }
}
