using System.Linq;

namespace GE_Command
{
   [Command]
   class DeleteCommand : BaseCommand
   {
      public override CommandID ID => CommandID.Delete;

      public override void DoCommand()
      {
         int objectsCount = GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly.Count;
         for (int i = objectsCount - 1; i >= 0; i--)
         {
            GE_VMObject.VM_BaseObject obj = GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly.ElementAt(i);

            if (!GE_ViewModel.DeskViewModel.Instance.SelectedObjects.IsObjectSelected(obj.ModelID))
               continue;

            deleteObject(obj);
         }
      }

      private void deleteObject(GE_VMObject.VM_BaseObject obj)
      {
         obj.DeleteUI();
         GE_ViewModel.DeskViewModel.Instance.SelectedObjects.DeSelectObject(obj.ModelID);
         GE_ViewModel.DeskViewModel.Instance.ObjectsViews.RemoveObjectWithID(obj.ModelID);
         GE_ViewModel.DeskViewModel.Instance.Model.Objects.RemoveObjectWithID(obj.ModelID);
      }
   }
}
