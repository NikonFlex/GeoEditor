namespace GE_Command
{
   [Command]
   class ZoomAllCommand : BaseCommand
   {
      public override CommandID ID => CommandID.ZoomAll;

      public override void DoCommand()
      {
         GE_Maths.BoundRect bRect = new();
         foreach (GE_GeomObject.BaseObject obj in GE_Model.Model.Instance.Objects.ObjectsReadOnly)
         {
            if (obj is GE_GeomObject.Segment seg)
            {
               bRect.AddPoint(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(seg.P1));
               bRect.AddPoint(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(seg.P2));
            }
         }

         GE_ViewModel.DeskViewModel.Instance.Transformator.SetTransformatorFromBoundRect(bRect);
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }
   }
}
