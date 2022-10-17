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

         bool _zoomOnSelected = VM.ObjectsViews.ObjectsReadOnly.
                                Any(obj => VM.SelectedObjects.IsObjectSelected(obj.ModelID));

         foreach (GE_VMObject.VM_BaseObject obj in VM.ObjectsViews.ObjectsReadOnly)
         {
            if (_zoomOnSelected && !VM.SelectedObjects.IsObjectSelected(obj.ModelID))
               continue;

            obj.GetAllWorldPoints().ForEach(point => bRect.AddPoint(point));
         }

         VM.Transformator.SetTransformatorFromBoundRect(bRect);
         VM.RefreshView();
      }
   }
}
