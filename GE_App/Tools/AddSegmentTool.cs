using System.Windows.Input;
using GE_Primitive;

namespace GE_Tool
{
   [Tool]
   class AddSegmentTool : BaseTool
   {
      private PrimPolyline _curSegment = null;
      private bool _isFirstPointSet = false;
      private bool _isShiftPressed = false;

      public override ToolID ID => ToolID.AddSegment;

      public override void MouseMove(MouseEventArgs e)
      {
         if (_isFirstPointSet)
         {
            _curSegment.Points[1] = calcSecondPoint(PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen)));
            GE_ViewModel.DeskViewModel.Instance.SetPhantomGeometry(_curSegment);
            GE_ViewModel.DeskViewModel.Instance.RefreshPhantom();
         }
      }

      public override void MouseDown(MouseButtonEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         if (!_isFirstPointSet)
            start(screenEventPos);
         else
            finish();
      }

      public override void KeyDown(KeyEventArgs e)
      {
         if (e.Key == Key.Escape)
            emergencyFinish();

         if (e.Key == Key.LeftShift)
            _isShiftPressed = true;
      }

      public override void KeyUp(KeyEventArgs e)
      {
         if (e.Key == Key.LeftShift)
            _isShiftPressed = false;
      }

      private void start(PrimPoint screenEventPos)
      {
         _curSegment = new();
         _curSegment.AddPoint(screenEventPos);
         _curSegment.AddPoint(screenEventPos);
         GE_ViewModel.DeskViewModel.Instance.SetPhantomGeometry(_curSegment);
         GE_ViewModel.DeskViewModel.Instance.RefreshPhantom(true);
         _isFirstPointSet = true;
      }

      private void finish()
      {
         GE_Model.Model.Instance.AddSegment(GE_ViewModel.DeskViewModel.Instance.Transformator.ScreenToWorld(_curSegment.Points[0]),
                                            GE_ViewModel.DeskViewModel.Instance.Transformator.ScreenToWorld(_curSegment.Points[1]));
         _isFirstPointSet = false;
         _curSegment = new();
         GE_ViewModel.DeskViewModel.Instance.SetPhantomGeometry(_curSegment);
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }

      private void emergencyFinish()
      {
         _isFirstPointSet = false;
         _curSegment = new();
         GE_ViewModel.DeskViewModel.Instance.SetPhantomGeometry(_curSegment);
         GE_ViewModel.DeskViewModel.Instance.RefreshView();
      }

      private PrimPoint calcSecondPoint(PrimPoint eventPos)
      {
         if (!_isShiftPressed)
            return eventPos;

         var deltaX = System.Math.Abs(eventPos.X - _curSegment.Points[0].X);
         var deltaY = System.Math.Abs(eventPos.Y - _curSegment.Points[0].Y);

         if (deltaX > deltaY)
            return new(eventPos.X, _curSegment.Points[0].Y);
         else
            return new(_curSegment.Points[0].X, eventPos.Y);
      }
   }
}
