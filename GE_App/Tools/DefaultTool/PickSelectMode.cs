using System.Windows.Input;
using System.Diagnostics;
using GE_Primitive;
using GE_ViewModel;

namespace GE_Tool
{
   class PickSelectMode : DefaultToolMode
   {
      private double _maxDistToSeg = 2.5;
      private bool _isCtrlPressed = false;

      public override void OnMouseMove(MouseEventArgs e)
      {
         reHoverObjects(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
      }

      public override void OnMouseUp(MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Right)
            return;
         reSelectObjects(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
      }

      public override void OnKeyDown(KeyEventArgs e)
      {
         _isCtrlPressed = e.Key == Key.LeftCtrl;
      }

      public override void OnKeyUp(KeyEventArgs e)
      {
         _isCtrlPressed = e.Key == Key.LeftCtrl;
      }

      private void reSelectObjects(PrimPoint point)
      {
         foreach (GE_VMObject.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (obj.DistTo(point) <= _maxDistToSeg)
            {
               DeskViewModel.Instance.SelectedObjects.SelectObject(obj.ModelID);
               obj.SetState(GE_VMObject.VMObjectState.Selected);  
            }
            else if (!_isCtrlPressed)
            {
               DeskViewModel.Instance.SelectedObjects.DeSelectObject(obj.ModelID);
               obj.SetState(GE_VMObject.VMObjectState.None);
            }
         }
      }

      private void reHoverObjects(PrimPoint point)
      {
         foreach (GE_VMObject.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (DeskViewModel.Instance.SelectedObjects.IsObjectSelected(obj.ModelID))
               continue;

            if (obj.DistTo(point) <= _maxDistToSeg)
               obj.SetState(GE_VMObject.VMObjectState.Hovered);
            else
               obj.SetState(GE_VMObject.VMObjectState.None);
         }
      }
   }
}
