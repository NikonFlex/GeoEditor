using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;

namespace GE_Tool
{
   class PickSelectMode : DefaultToolMode
   {
      public override void OnMouseUp(MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Right)
            return;
         reSelectObjects(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
      }

      private void reSelectObjects(PrimPoint point)
      {
         foreach (GE_VM_Object.VM_BaseObject obj in DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (obj is GE_VM_Object.VM_Segment segment)
               selectSegment(point, segment);

         }

         DeskViewModel.Instance.RefreshView();
      }

      private void selectSegment(PrimPoint point, GE_VM_Object.VM_Segment segment)
      {
         double distToSeg = GE_Maths.GE_Math.PointToSegmentDist(point, DeskViewModel.Instance.Transformator.WorldToScreen(segment.Segment.P1),
                                                                       DeskViewModel.Instance.Transformator.WorldToScreen(segment.Segment.P2));
         if (distToSeg < 5)
         {
            if (segment.IsSelected)
               segment.DeSelect();
            else
               segment.Select();
         }
         else if (!IsCtrlPressed)
         {
            segment.DeSelect();
         }
      }
   }
}
