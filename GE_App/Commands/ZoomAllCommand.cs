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

         bool _zoomOnSelected = GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly.
                                Any(obj => GE_ViewModel.DeskViewModel.Instance.SelectedObjects.IsObjectSelected(obj.ModelID));

         foreach (GE_VMObject.VM_BaseObject obj in GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (_zoomOnSelected && !GE_ViewModel.DeskViewModel.Instance.SelectedObjects.IsObjectSelected(obj.ModelID))
               continue;

            obj.GetAllWorldPoints().ForEach(point => bRect.AddPoint(point));
         }

         GE_ViewModel.DeskViewModel.Instance.Transformator.SetTransformatorFromBoundRect(bRect);
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }
   }
}
