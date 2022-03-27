using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;
using System.Linq;

namespace GE_Tool
{
   class AreaSelectMode : DefaultToolMode
   {
      private bool _isOriginPointSet = false;

      public override void OnMouseMove(MouseEventArgs e)
      {
         if (_isOriginPointSet)
         {
            DeskViewModel.Instance.SelectArea.SetSecondPoint(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
            reHoverObjects();
         }
      }

      public override void OnMouseDown(MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Left)
            return;

         start(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
      }

      public override void OnMouseUp(MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Left)
            return;

         finish();
      }

      private void start(PrimPoint screenEventPos)
      {
         DeskViewModel.Instance.SelectArea.SetControlPoint(screenEventPos);
         _isOriginPointSet = true;
      }

      private void finish()
      {
         reSelectObjects();
         _isOriginPointSet = false;
         DeskViewModel.Instance.SelectArea.Clear();
      }

      private void reSelectObjects()
      {
         foreach (GE_VMObject.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (DeskViewModel.Instance.SelectArea.IsInside(obj.GetAllScreenPoints()))
            {
               DeskViewModel.Instance.SelectedObjects.SelectObject(obj.ModelID);
               obj.SetState(GE_VMObject.VMObjectState.Selected);
            }
            else
            {
               if (!IsCtrlPressed)
               {
                  DeskViewModel.Instance.SelectedObjects.DeSelectObject(obj.ModelID);
                  obj.SetState(GE_VMObject.VMObjectState.None);
               }
            }
         }
      }

      private void reHoverObjects()
      {
         foreach (GE_VMObject.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (DeskViewModel.Instance.SelectedObjects.IsObjectSelected(obj.ModelID))
               continue;

            if (DeskViewModel.Instance.SelectArea.IsInside(obj.GetAllScreenPoints()))
               obj.SetState(GE_VMObject.VMObjectState.Hovered);
            else
               obj.SetState(GE_VMObject.VMObjectState.None);
         }
      }
   }
}
