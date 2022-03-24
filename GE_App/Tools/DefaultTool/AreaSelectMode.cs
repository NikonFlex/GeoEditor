using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;

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
            DeskViewModel.Instance.RefreshSelectArea();
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
         DeskViewModel.Instance.SetSelectAreaGeometry(screenEventPos, screenEventPos);
         DeskViewModel.Instance.RefreshSelectArea(true);
         _isOriginPointSet = true;
      }

      private void finish()
      {
         reSelectObjects();
         _isOriginPointSet = false;
         DeskViewModel.Instance.SetSelectAreaGeometry(new(), new());
         DeskViewModel.Instance.RefreshView();
      }

      private void reSelectObjects()
      {
         foreach (GE_VM_Object.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (obj is GE_VM_Object.VM_Segment segment)
               selectSegment(segment);
         }
      }

      private void selectSegment(GE_VM_Object.VM_Segment segment)
      {
         if (DeskViewModel.Instance.SelectArea.IsSegmentInside(segment))
            segment.Select();
         else if (!IsCtrlPressed)
            segment.DeSelect();
      }
   }
}
