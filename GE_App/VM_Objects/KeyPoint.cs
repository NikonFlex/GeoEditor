using GE_Primitive;
using System.Collections.Generic;

namespace GE_VMObject
{
   class KeyPoint
   {
      public PrimPoint Point { get; private set; }
      public VM_BaseObject Object { get; private set; }

      private double _radius = 5;

      public KeyPoint(PrimPoint point, VM_BaseObject obj)
      {
         Point = point;
         Object = obj;
      }

      public void Draw(System.Windows.Controls.Canvas screen)
      {
         List<PrimPoint> pointsList = new();

         PrimPoint p1 = new();
         PrimPoint p2 = new(Point.X + _radius, Point.Y);
         for (float a = 72; a <= 360; a += 72)
         {
            p1.X = p2.X;
            p1.Y = p2.Y;
            pointsList.Add(p1);
            p2.X = _radius * System.Math.Cos(a * System.Math.PI / 180) + Point.X;
            p2.Y = _radius * System.Math.Sin(a * System.Math.PI / 180) + Point.Y;
            pointsList.Add(p2);
         }

         screen.Children.Add(GeoEditor.Utils.CreatePolygon(pointsList, 1, System.Windows.Media.Brushes.DarkOrange));
      }
   }
}
