namespace GE_Command
{
   [Command]
   class DeleteCommand : BaseCommand
   {
      public override CommandID ID => CommandID.Delete;

      public override void DoCommand()
      {
         int objectsCount = GE_Model.Model.Instance.Objects.ObjectsReadOnly.Count;
         for (int i = objectsCount; i >= 0; i--)
         {
            if (!GE_ViewModel.DeskViewModel.Instance.IsObjectSelectedAt(i))
               continue;
            
            GE_ViewModel.DeskViewModel.Instance.DeSelectObjectAt(i);
            GE_Model.Model.Instance.RemoveObjectAt(i);
         }
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }
   }
}
