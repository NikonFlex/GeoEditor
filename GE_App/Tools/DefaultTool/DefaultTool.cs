using System.Windows.Input;

namespace GE_Tool
{
   [Tool]
   class DefaultTool : BaseTool
   {
      public override ToolID ID => ToolID.Select;
      private DefaultToolMode _activeMode = null;

      public override void MouseMove(MouseEventArgs e)
      {
         _activeMode?.OnMouseMove(e);
      }

      public override void MouseDown(MouseButtonEventArgs e)
      {
         if (e.ChangedButton == MouseButton.Left)
            _activeMode = new AreaSelectMode();
         else if (e.ChangedButton == MouseButton.Right)
            _activeMode = new PickSelectMode();
         else if (e.ChangedButton == MouseButton.Middle)
            _activeMode = new MoveViewMode();

         _activeMode.OnMouseDown(e);
      }

      public override void MouseUp(MouseButtonEventArgs e)
      {
         _activeMode?.OnMouseUp(e);
         _activeMode = null;
      }

      public override void MouseWheel(MouseWheelEventArgs e)
      {
         _activeMode = new ScaleViewMode();
         _activeMode.OnMouseWheel(e);
      }

      public override void KeyDown(KeyEventArgs e)
      {
         if (_activeMode is null)
            return;

         if (e.Key == Key.LeftCtrl)
            _activeMode.IsCtrlPressed = true;
      }

      public override void KeyUp(KeyEventArgs e)
      {
         if (_activeMode is null)
            return;

         if (e.Key == Key.LeftCtrl)
            _activeMode.IsCtrlPressed = false;
      }
   }
}
