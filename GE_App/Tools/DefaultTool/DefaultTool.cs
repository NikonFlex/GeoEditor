using System.Windows.Input;

namespace GE_Tool
{
   [Tool]
   class DefaultTool : BaseTool
   {
      public override ToolID ID => ToolID.Default;
      private DefaultToolMode _activeMode;

      public DefaultTool()
      {
         _activeMode = new PickSelectMode();
      }

      protected override void MouseMove(MouseEventArgs e)
      {
         _activeMode?.OnMouseMove(e);
      }

      protected override void MouseDown(MouseButtonEventArgs e)
      {
         if (e.ChangedButton == MouseButton.Left)
            _activeMode = new AreaSelectMode();
         else if (e.ChangedButton == MouseButton.Right)
            _activeMode = new PickSelectMode();
         else if (e.ChangedButton == MouseButton.Middle)
            _activeMode = new MoveViewMode();

         _activeMode.OnMouseDown(e);
      }

      protected override void MouseUp(MouseButtonEventArgs e)
      {
         _activeMode?.OnMouseUp(e);
         _activeMode = new PickSelectMode();
      }

      protected override void MouseWheel(MouseWheelEventArgs e)
      {
         _activeMode = new ScaleViewMode();
         _activeMode.OnMouseWheel(e);
         _activeMode = new PickSelectMode();
      }

      protected override void KeyDown(KeyEventArgs e)
      {
         _activeMode.OnKeyDown(e);
      }

      protected override void KeyUp(KeyEventArgs e)
      {
         _activeMode.OnKeyUp(e);
      }
   }
}
