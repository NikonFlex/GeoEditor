using System.Linq;

namespace GE_Command
{
   [Command]
   class ZoomAllCommand : BaseCommand
   {
      public override CommandID ID => CommandID.ZoomAll;

      public override void DoCommand()
      {
         GE_Maths.BoundRect bRect = new();

         bool _zoomOnSelected = GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly.Any(obj => obj.IsSelected);

         foreach (GE_VM_Object.VM_BaseObject obj in GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (_zoomOnSelected && !obj.IsSelected)
               continue;

            if (obj is GE_VM_Object.VM_Segment segment)
            {
               bRect.AddPoint(segment.Segment.P1);
               bRect.AddPoint(segment.Segment.P2);
            }
         }

         GE_ViewModel.DeskViewModel.Instance.Transformator.SetTransformatorFromBoundRect(bRect);
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }
   }
}
