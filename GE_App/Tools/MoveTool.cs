using System.Windows.Input;
using System.Collections.Generic;
using GE_Primitive;

namespace GE_Tool
{
   [Tool]
   class MoveTool : BaseTool
   {
      public override ToolID ID => ToolID.Move;

      private List<GE_VMObject.MovePoint> _movePoints = new();
      private GE_VMObject.SnapPoint _snapPoint;

      protected override void Activate()
      {
         calcMovePoints();
      }

      protected override void DeActivate()
      {
         foreach (GE_VMObject.MovePoint movePoint in _movePoints)
            movePoint.DeleteUI();

         _movePoints.Clear();
         clearSnapPoint();
      }

      protected override void MouseMove(MouseEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));

         if (e.LeftButton == MouseButtonState.Pressed)
            movePoints(screenEventPos);
         else
            reHoverMovePoints(screenEventPos);
      }

      protected override void MouseDown(MouseButtonEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         start(screenEventPos);
      }

      protected override void MouseUp(MouseButtonEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         finishMove(screenEventPos);
      }

      protected override void KeyUp(KeyEventArgs e)
      {
         if (!_isCtrlPressed)
            clearSnapPoint();
      }

      private void movePoints(PrimPoint eventPos)
      {
         foreach (GE_VMObject.MovePoint movePoint in _movePoints)
         {
            if (movePoint.IsActive)
            {
               calcSnapPoint(movePoint, eventPos);

               if (_isCtrlPressed)
                  movePoint.SetPoint(eventPos);
               else
                  movePoint.SetPoint(snapMove(eventPos));
            }
         }
      }

      private void start(PrimPoint screenEventPos)
      {
         reSelectMovePoints(screenEventPos);
         deleteNotActivatedMovePoints();
      }

      private PrimPoint snapMove(PrimPoint eventPos)
      {
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

      private void finishMove(PrimPoint eventPos)
      {
         foreach (GE_VMObject.MovePoint movePoint in _movePoints)
            movePoint.DeleteUI();

         _movePoints.Clear();
         clearSnapPoint();
         calcMovePoints();
         reHoverMovePoints(eventPos);
      }

      private void reSelectMovePoints(PrimPoint point)
      {
         foreach (GE_VMObject.MovePoint movePoint in _movePoints)
         {
            if (movePoint.Coord.DistTo(point) <= movePoint.Radius)
               movePoint.SetState(GE_VMObject.VMObjectState.Selected);
            else
               movePoint.SetState(GE_VMObject.VMObjectState.None);
         }
      }

      private void deleteNotActivatedMovePoints()
      {
         List<GE_VMObject.MovePoint> movePoints = new();
         foreach (GE_VMObject.MovePoint movePoint in _movePoints)
         {
            if (movePoint.IsActive)
               movePoints.Add(movePoint);
            else
               movePoint.DeleteUI();
         }
         _movePoints.Clear();
         _movePoints = movePoints;
      }

      private void reHoverMovePoints(PrimPoint point)
      {
         foreach (GE_VMObject.MovePoint keyPoint in _movePoints)
         {
            if (keyPoint.IsActive)
               continue;

            if (keyPoint.Coord.DistTo(point) <= keyPoint.Radius)
               keyPoint.SetState(GE_VMObject.VMObjectState.Hovered);
            else
               keyPoint.SetState(GE_VMObject.VMObjectState.None);
         }
      }

      private void calcMovePoints()
      {
         _movePoints.Clear();
         foreach (GE_VMObject.VM_BaseObject obj in GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
            _movePoints.AddRange(obj.GetMovePoints());

         foreach (GE_VMObject.MovePoint keyPoint in _movePoints)
            GE_ViewModel.DeskViewModel.Instance.Screen.Children.Add(keyPoint.CreateView());
      }

      private void calcSnapPoint(GE_VMObject.MovePoint movePoint, PrimPoint eventPos)
      {
         clearSnapPoint();
         _snapPoint = GE_ViewModel.DeskViewModel.Instance.GetSnapPoint(eventPos, movePoint.Object);
      }

      private void clearSnapPoint()
      {
         _snapPoint?.DeActivate();
         _snapPoint = null;
      }
   }
}
