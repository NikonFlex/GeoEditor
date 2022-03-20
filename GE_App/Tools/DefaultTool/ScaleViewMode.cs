using System.Windows.Input;
using GE_Primitive;
using GE_ViewModel;

namespace GE_Tool
{
   class ScaleViewMode : DefaultToolMode
   {     
      public override void OnMouseWheel(MouseWheelEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(DeskViewModel.Instance.Screen));
         PrimPoint worldEventPos = DeskViewModel.Instance.Transformator.ScreenToWorld(screenEventPos);
         
         if (e.Delta > 0)
            DeskViewModel.Instance.Transformator.Scale *= 0.95;
         else
            DeskViewModel.Instance.Transformator.Scale *= 1.095;

         GE_Maths.Transformator tr = DeskViewModel.Instance.Transformator;
         DeskViewModel.Instance.Transformator.Pivot.X = worldEventPos.X - (screenEventPos.X - tr.ScreenWidth / 2) * tr.Scale;
         DeskViewModel.Instance.Transformator.Pivot.Y = worldEventPos.Y + (screenEventPos.Y - tr.ScreenHeight / 2) * tr.Scale;

         DeskViewModel.Instance.RefreshView();
      }
   }
}
