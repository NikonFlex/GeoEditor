using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;

namespace GE_Tool
{
   class PickSelectMode : DefaultToolMode
   {
      private double _maxDistToSeg = 2.5;

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

      private void reSelectObjects(PrimPoint point)
      {
         foreach (GE_VMObject.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (obj.DistTo(point) <= _maxDistToSeg)
               DeskViewModel.Instance.SelectObject(obj.ModelID);
            else if (!_isCtrlPressed)
               DeskViewModel.Instance.DeSelectObject(obj.ModelID);
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
