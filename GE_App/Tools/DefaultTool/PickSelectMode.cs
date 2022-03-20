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
         int counter = 0;
         foreach (GE_GeomObject.BaseObject obj in GE_Model.Model.Instance.Objects.ObjectsReadOnly)
         {
            if (obj is GE_GeomObject.Segment seg)
               selectSegment(point, seg, counter);
  
            counter++;
         }

         DeskViewModel.Instance.RefreshView();
      }

      private void selectSegment(PrimPoint point, GE_GeomObject.Segment seg, int objectI)
      {
         double distToSeg = GE_Maths.GE_Math.PointToSegmentDist(point, DeskViewModel.Instance.Transformator.WorldToScreen(seg.P1),
                                                                       DeskViewModel.Instance.Transformator.WorldToScreen(seg.P2));
         if (distToSeg < 5)
         {
            if (DeskViewModel.Instance.IsObjectSelectedAt(objectI))
               DeskViewModel.Instance.DeSelectObjectAt(objectI);
            else
               DeskViewModel.Instance.SelectObjectAt(objectI);
         }
         else if (!IsCtrlPressed)
            DeskViewModel.Instance.DeSelectObjectAt(objectI);
      }
   }
}
