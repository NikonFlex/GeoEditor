using System.Windows.Input;
using System.Collections.Generic;
using GE_Primitive;

namespace GE_Tool
{
   [Tool]
   class MoveTool : BaseTool
   {
      public override ToolID ID => ToolID.Move;

      private List<GE_VMObject.KeyPoint> _movePoints = new();
      private GE_VMObject.KeyPoint _snapPoint;

      protected override void Activate()
      {
         calcMovePoints();
      }

      protected override void DeActivate()
      {
         foreach (GE_VMObject.KeyPoint keyPoint in _movePoints)
            keyPoint.DeleteUI();

         _movePoints.Clear();
         _snapPoint = null;
      }

      protected override void MouseMove(MouseEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));

         if (e.LeftButton == MouseButtonState.Pressed)
            movePoints(screenEventPos);
         else
            reHoverKeyPoints(screenEventPos);
      }

      protected override void MouseDown(MouseButtonEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         reSelectKeyPoints(screenEventPos);
      }

      protected override void MouseUp(MouseButtonEventArgs e)
      {
         PrimPoint screenEventPos = PrimPoint.FromWindowsPoint(e.GetPosition(GE_ViewModel.DeskViewModel.Instance.Screen));
         finishMove(screenEventPos);
      }

      private void movePoints(PrimPoint eventPos)
      {
         foreach (GE_VMObject.KeyPoint movePoint in _movePoints)
         {
            if (movePoint.IsActive)
            {
               calcSnapPoint(movePoint, eventPos);

               if (_isCtrlPressed)
                  movePoint.SetPoint(snapMove(eventPos));
               else
                  movePoint.SetPoint(eventPos);
            }
         }
      }

      private PrimPoint snapMove(PrimPoint eventPos)
      {
         if (_snapPoint is not null)
            return _snapPoint.Coord;
         else
            return eventPos;
      }

      private void finishMove(PrimPoint eventPos)
      {
         foreach (GE_VMObject.KeyPoint movePoint in _movePoints)
            movePoint.SetState(GE_VMObject.VMObjectState.None);
         reHoverKeyPoints(eventPos);
      }

      private void reSelectKeyPoints(PrimPoint point)
      {
         foreach (GE_VMObject.KeyPoint MovePoint in _movePoints)
         {
            if (MovePoint.Coord.DistTo(point) <= MovePoint.Radius)
               MovePoint.SetState(GE_VMObject.VMObjectState.Selected);
            else
               MovePoint.SetState(GE_VMObject.VMObjectState.None);
         }
      }

      private void reHoverKeyPoints(PrimPoint point)
      {
         foreach (GE_VMObject.KeyPoint keyPoint in _movePoints)
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

         foreach (GE_VMObject.KeyPoint keyPoint in _movePoints)
            GE_ViewModel.DeskViewModel.Instance.AddKeyPoint(keyPoint);
      }

      private void calcSnapPoint(GE_VMObject.KeyPoint movePoint, PrimPoint eventPos)
      {
         _snapPoint = null;

         foreach (GE_VMObject.VM_BaseObject obj in GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (obj == movePoint.Object)
               continue;

            GE_VMObject.KeyPoint snapPoint = obj.GetSnapPoint(eventPos);

            if (_snapPoint is null)
            {
               _snapPoint = snapPoint;
               continue;
            }

            if (snapPoint is not null && snapPoint.Coord.DistTo(eventPos) <= _snapPoint.Coord.DistTo(eventPos))
               _snapPoint = snapPoint;
         }
      }
   }
}
