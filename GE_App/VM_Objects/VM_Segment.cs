using GE_Primitive;
using System.Collections.Generic;
using System.Linq;

namespace GE_VM_Object
{
   class VM_Segment : VM_BaseObject
   {
      public GE_GeomObject.Segment Segment => (GE_GeomObject.Segment)GE_Model.Model.Instance.Objects.ObjectsReadOnly.First(obj => obj.ID == _modelID);

      public VM_Segment(int id)
      {
         _modelID = id;
      }

      public override List<PrimPoint> GetMovePoints()
      {
         throw new System.NotImplementedException();
      }

      public override List<PrimPoint> GetSnapPoints()
      {
         throw new System.NotImplementedException();
      }

      public override void Draw(System.Windows.Controls.Canvas screen)
      {
         screen.Children.Add(GeoEditor.Utils.createSegmentLine(GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P1),
                                                               GE_ViewModel.DeskViewModel.Instance.Transformator.WorldToScreen(Segment.P2), 1.5,
                                                               IsSelected ? System.Windows.Media.Brushes.Crimson : System.Windows.Media.Brushes.Black));
      }
   }
}
