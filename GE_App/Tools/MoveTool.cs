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
      private List<GE_VMObject.KeyPoint> _snapPoints = new();

      protected override void Activate()
      {
         calcMovePoints();
      }

      protected override void DeActivate()
      {
         foreach (GE_VMObject.KeyPoint keyPoint in _movePoints)
            keyPoint.DeleteUI();

         _movePoints.Clear();
         _snapPoints.Clear();
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
               calcSnapPoints(movePoint, eventPos);

               if (_isCtrlPressed)
                  movePoint.SetPoint(snapMove(eventPos));
               else
                  movePoint.SetPoint(eventPos);
            }
         }
      }

      private PrimPoint snapMove(PrimPoint eventPos)
      {
         if (_snapPoints.Count != 0)
            return findClosestSnapPoint(eventPos).Point;
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
            if (MovePoint.Point.DistTo(point) <= MovePoint.Radius)
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

            if (keyPoint.Point.DistTo(point) <= keyPoint.Radius)
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

      private void calcSnapPoints(GE_VMObject.KeyPoint movePoint, PrimPoint eventPos)
      {
         _snapPoints.Clear();
         foreach (GE_VMObject.VM_BaseObject obj in GE_ViewModel.DeskViewModel.Instance.ObjectsViews.ObjectsReadOnly)
         {
            if (obj == movePoint.Object)
               continue;

            GE_VMObject.KeyPoint snapPoint = obj.GetSnapPoint(eventPos);
            if (snapPoint is not null)
               _snapPoints.Add(snapPoint);
         }
      }

      private GE_VMObject.KeyPoint findClosestSnapPoint(PrimPoint eventPos)
      {
         GE_VMObject.KeyPoint closestSnapPoint = _snapPoints[0];
         foreach (GE_VMObject.KeyPoint snapPoint in _snapPoints)
         {
            if (snapPoint.Point.DistTo(eventPos) < closestSnapPoint.Point.DistTo(eventPos))
               closestSnapPoint = snapPoint;
         }

         return closestSnapPoint;
      }
   }
}
