using System.Linq;

namespace GE_Command
{
   [Command]
   class DeleteCommand : BaseCommand
   {
      public override CommandID ID => CommandID.Delete;

      public override void DoCommand()
      {
         int objectsCount = VM.ObjectsViews.ObjectsReadOnly.Count;
         for (int i = objectsCount - 1; i >= 0; i--)
         {
            GE_VMObject.VM_BaseObject obj = VM.ObjectsViews.ObjectsReadOnly.ElementAt(i);

            if (!VM.SelectedObjects.IsObjectSelected(obj.ModelID))
               continue;

            VM.DeleteObject(obj.ModelID);
         }
      }
   }
}
