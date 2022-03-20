using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;

namespace GE_Tool
{
   class AreaSelectMode : DefaultToolMode
   {
      private bool _isOriginPointSet = false;

      public override void OnMouseMove(MouseEventArgs e)
      {
         if (_isOriginPointSet)
         {
            DeskViewModel.Instance.SelectArea.SetSecondPoint(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
            DeskViewModel.Instance.RefreshSelectArea();
         }
      }

      public override void OnMouseDown(MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Left)
            return;

         start(PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen)));
      }

      public override void OnMouseUp(MouseButtonEventArgs e)
      {
         if (e.ChangedButton != MouseButton.Left)
            return;

         finish();
      }

      private void start(PrimPoint screenEventPos)
      {
         DeskViewModel.Instance.SetSelectAreaGeometry(screenEventPos, screenEventPos);
         DeskViewModel.Instance.RefreshSelectArea(true);
         _isOriginPointSet = true;
      }

      private void finish()
      {
         reSelectObjects();
         _isOriginPointSet = false;
         DeskViewModel.Instance.SetSelectAreaGeometry(new(), new());
         DeskViewModel.Instance.RefreshView();
      }

      private void reSelectObjects()
      {
         int counter = 0;
         foreach (GE_GeomObject.BaseObject obj in GE_Model.Model.Instance.Objects.ObjectsReadOnly)
         {
            if (obj is GE_GeomObject.Segment seg)
               selectSegment(seg, counter);

            counter++;
         }
      }

      private void selectSegment(GE_GeomObject.Segment seg, int objectI)
      {
         if (DeskViewModel.Instance.SelectArea.IsSegmentInside(seg))
            DeskViewModel.Instance.SelectObjectAt(objectI);
         else if (!IsCtrlPressed)
            DeskViewModel.Instance.DeSelectObjectAt(objectI);
      }
   }
}
