using System.Linq;

namespace GE_Command
{
   [Command]
   class DeleteCommand : BaseCommand
   {
      public override CommandID ID => CommandID.Delete;

      public override void DoCommand()
      {
         int objectsCount = GE_Model.Model.Instance.Objects.ObjectsReadOnly.Count;
         for (int i = objectsCount - 1; i >= 0; i--)
         {
            GE_VM_Object.VM_BaseObject obj = GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly.ElementAt(i);

            if (!obj.IsSelected)
               continue;

            GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly.ElementAt(i).DeSelect();
            GE_ViewModel.DeskViewModel.Instance.ObjectsViews.RemoveObjectWithID(obj.ModelID);
         }
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }
   }
}
