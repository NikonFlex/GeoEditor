using System.Diagnostics;
using System.Windows.Input;
using GE_Primitive;

namespace GE_Tool
{
   [Tool]
   class AddSegmentTool : BaseTool
   {
      private PrimPolyline _curSegment = null;
      private bool _isFirstPointSet = false;
      private GE_VMObject.SnapPoint _snapPoint;

      public override ToolID ID => ToolID.AddSegment;

      protected override void MouseMove(MouseEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         
         if (_isFirstPointSet)
         {
            _curSegment.Points[1] = calcSecondPoint(screenEventPos);
            GE_ViewModel.DeskViewModel.Instance.Phantom.SetGeometry(_curSegment);
         }
         else
         {
            calcFirstPoint(screenEventPos);
         }
      }

      protected override void MouseDown(MouseButtonEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         if (!_isFirstPointSet)
            start(screenEventPos);
         else
            finish();
      }

      protected override void KeyDown(KeyEventArgs e)
      {
         if (e.Key == Key.Escape)
            emergencyFinish();
      }

      protected override void KeyUp(KeyEventArgs e)
      {
         if (!_isCtrlPressed)
            clearSnapPoint();
      }

      protected void start(PrimPoint screenEventPos)
      {
         _curSegment = new();
         if (_snapPoint is not null)
         {
            _curSegment.AddPoint(_snapPoint.Coord);
            _curSegment.AddPoint(_snapPoint.Coord);
         }
         else
         {
            _curSegment.AddPoint(screenEventPos);
            _curSegment.AddPoint(screenEventPos);
         }
         GE_ViewModel.DeskViewModel.Instance.Phantom.SetGeometry(_curSegment);
         _isFirstPointSet = true;
      }

      private void finish()
      {
         GE_ViewModel.DeskViewModel.Instance.AddSegment(_curSegment.Points[0], _curSegment.Points[1]);
         _isFirstPointSet = false;
         _curSegment = new();
         GE_ViewModel.DeskViewModel.Instance.Phantom.SetGeometry(_curSegment);
      }

      private void emergencyFinish()
      {
         _isFirstPointSet = false;
         _curSegment = new();
         GE_ViewModel.DeskViewModel.Instance.Phantom.SetGeometry(_curSegment);
      }

      private PrimPoint calcFirstPoint(PrimPoint eventPos)
      {
         if (_isCtrlPressed)
            return eventPos;
         return calcSnapPoint(eventPos);
      }

      private PrimPoint calcSecondPoint(PrimPoint eventPos)
      {
         clearSnapPoint();
         if (_isShiftPressed)
            return calcStraightPoint(eventPos);
         else if (_isCtrlPressed)
            return eventPos;
         return calcSnapPoint(eventPos);
      }

      private PrimPoint calcStraightPoint(PrimPoint eventPos)
      {
         double deltaX = System.Math.Abs(eventPos.X - _curSegment.Points[0].X);
         double deltaY = System.Math.Abs(eventPos.Y - _curSegment.Points[0].Y);

         if (deltaX > deltaY)
            return new(eventPos.X, _curSegment.Points[0].Y);
         else
            return new(_curSegment.Points[0].X, eventPos.Y);
      }

      private PrimPoint calcSnapPoint(PrimPoint eventPos)
      {            
         clearSnapPoint();
         _snapPoint = GE_ViewModel.DeskViewModel.Instance.GetSnapPoint(eventPos);

         if (_snapPoint is not null)
         {
            _snapPoint.Activate();
            return _snapPoint.Coord;
         }
         else
         {
            return eventPos;
         }
      }

      private void clearSnapPoint()
      {
         _snapPoint?.DeActivate();
         _snapPoint = null;
      }
   }
}
