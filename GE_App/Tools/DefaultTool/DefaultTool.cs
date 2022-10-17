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
         switch (e.ChangedButton)
         {
            case MouseButton.Left:
               _activeMode = new AreaSelectMode();
               break;
            case MouseButton.Right:
               _activeMode = new PickSelectMode();
               break;
            case MouseButton.Middle:
               _activeMode = new MoveViewMode();
               break;
         }
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

      protected override void KeyDown(KeyEventArgs e) => _activeMode.OnKeyDown(e);
      protected override void KeyUp(KeyEventArgs e) => _activeMode.OnKeyUp(e);
   }
}
