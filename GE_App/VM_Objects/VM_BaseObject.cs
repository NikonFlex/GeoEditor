using System.Collections.Generic;

namespace GE_VM_Object
{
   abstract class VM_BaseObject
   {
      public bool IsSelected { get; private set; }

      protected int _modelID;

      public void Select() => IsSelected = true;
      public void DeSelect() => IsSelected = false;
      public int ModelID => _modelID;

      public abstract List<GE_Primitive.PrimPoint> GetMovePoints();
      public abstract List<GE_Primitive.PrimPoint> GetSnapPoints();
      public abstract void Draw(System.Windows.Controls.Canvas screen);
   }
}
