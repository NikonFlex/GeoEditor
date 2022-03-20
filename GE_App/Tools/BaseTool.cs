using System.Windows.Input;

namespace GE_Tool
{
   public class ToolAttribute : System.Attribute
   { }

   enum ToolID
   {
      Select,
      AddSegment,
      AddLine,
      AddCircle,
      AddRay
   }

   abstract class BaseTool
   {
      public BaseTool()
      {
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseMove += onMouseMove;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseDown += onMouseDown;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseUp += onMouseUp;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseWheel += onMouseWheel;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnKeyDown += onKeyDown;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnKeyUp += onKeyUp;
      }

      ~BaseTool()
      {
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseMove -= onMouseMove;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseDown -= onMouseDown;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseUp -= onMouseUp;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnMouseWheel -= onMouseWheel;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnKeyDown -= onKeyDown;
         GE_Service.ServicesContainer.Get<GE_Service.IEventService>().OnKeyUp -= onKeyUp;
      }

      protected bool _isActive;

      public abstract ToolID GetID();
      public bool IsActive() => _isActive;
      public void Activate() => _isActive = true;
      public void DeActivate() => _isActive = false;

      private void onMouseMove(MouseEventArgs e)
      {
         if (_isActive)
            MouseMove(e);
      }

      private void onMouseDown(MouseButtonEventArgs e)
      {
         if (_isActive)
            MouseDown(e);
      }

      private void onMouseUp(MouseButtonEventArgs e)
      {
         if (_isActive)
            MouseUp(e);
      }

      private void onMouseWheel(MouseWheelEventArgs e)
      {
         if (_isActive)
            MouseWheel(e);
      }

      private void onKeyDown(KeyEventArgs e)
      {
         if (_isActive)
            KeyDown(e);
      }

      private void onKeyUp(KeyEventArgs e)
      {
         if (_isActive)
            KeyUp(e);
      }

      public virtual void MouseMove(MouseEventArgs e) { }
      public virtual void MouseDown(MouseButtonEventArgs e) { }
      public virtual void MouseUp(MouseButtonEventArgs e) { }
      public virtual void MouseWheel(MouseWheelEventArgs e) { }
      public virtual void KeyDown(KeyEventArgs e) { }
      public virtual void KeyUp(KeyEventArgs e) { }
   }
}
