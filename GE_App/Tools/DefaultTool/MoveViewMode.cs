using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;

namespace GE_Tool
{
   class MoveViewMode : DefaultToolMode
   {
      private PrimPoint _prevMousePos = null;
      public override void OnMouseMove(MouseEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen));
         PrimPoint prevGlobalEventPos = DeskViewModel.Instance.Transformator.ScreenToWorld(_prevMousePos);

         GE_Maths.Transformator tr = DeskViewModel.Instance.Transformator;
         DeskViewModel.Instance.Transformator.Pivot.X = prevGlobalEventPos.X - (screenEventPos.X - tr.ScreenWidth / 2) * tr.Scale;
         DeskViewModel.Instance.Transformator.Pivot.Y = prevGlobalEventPos.Y + (screenEventPos.Y - tr.ScreenHeight / 2) * tr.Scale;

         DeskViewModel.Instance.RefreshView();
         _prevMousePos = screenEventPos;
      }

      public override void OnMouseDown(MouseButtonEventArgs e)
      {
         _prevMousePos = PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen));
      }
   }
}
