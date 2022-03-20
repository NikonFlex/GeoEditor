using System.Collections.Generic;

namespace GE_Primitive
{
   class PrimPolyline
   {
      private List<PrimPoint> _points = new();

      public PrimPolyline() { }

      public List<PrimPoint> Points => _points;

      public PrimPolyline(List<PrimPoint> pointsList)
      {
         pointsList.ForEach(point => _points.Add(point));
      }

      public void AddPoint(PrimPoint newPoint)
      {
         _points.Add(newPoint);
      }
   }
}
