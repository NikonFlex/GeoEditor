﻿using System.Windows.Input;
using System.Diagnostics;

namespace GE_Tool
{
   public class ToolAttribute : System.Attribute
   { }

   enum ToolID
   {
      Default,
      AddSegment,
      Move
   }

   abstract class BaseTool
   {
      protected bool _isActive = false;
      protected bool _isCtrlPressed = false;
      protected bool _isShiftPressed = false;

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

      public abstract ToolID ID { get; }
      public bool IsActive => _isActive;
      protected GE_ViewModel.DeskViewModel VM => GE_ViewModel.DeskViewModel.Instance;

      public void OnActivate()
      {
         _isActive = true;
         Activate();
      }

      public void OnDeActivate()
      {
         _isActive = false;
         DeActivate();
      }

      protected virtual void Activate() { }
      protected virtual void DeActivate() { }
      protected virtual void MouseMove(MouseEventArgs e) { }
      protected virtual void MouseDown(MouseButtonEventArgs e) { }
      protected virtual void MouseUp(MouseButtonEventArgs e) { }
      protected virtual void MouseWheel(MouseWheelEventArgs e) { }
      protected virtual void KeyDown(KeyEventArgs e) { }
      protected virtual void KeyUp(KeyEventArgs e) { }

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
         if (e.Key == Key.LeftCtrl)
            _isCtrlPressed = true;
         if (e.Key == Key.LeftShift)
            _isShiftPressed = true;
         
         if (_isActive)
            KeyDown(e);
      }

      private void onKeyUp(KeyEventArgs e)
      {
         if (e.Key == Key.LeftCtrl)
            _isCtrlPressed = false;
         if (e.Key == Key.LeftShift)
            _isShiftPressed = false;
         
         if (_isActive)
            KeyUp(e);
      }
   }
}
