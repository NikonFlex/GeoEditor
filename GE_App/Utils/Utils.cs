using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;

namespace GeoEditor
{
   class GE_ObjectsCollection
   {
      private List<GE_GeomObject.BaseObject> _objects = new();

      public IReadOnlyCollection<GE_GeomObject.BaseObject> ObjectsReadOnly => _objects;
      public int AddObject(GE_GeomObject.BaseObject newObject)
      {
         _objects.Add(newObject);
         return _objects.Count - 1;
      }
      public void RemoveObject(int index)
      {
         _objects.RemoveAt(index);
      }
   }

   static class Utils
   {
      public static Line createSegmentLine(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2, SolidColorBrush brush)
      {
         Line newLine = new Line();
         newLine.X1 = p1.X;
         newLine.Y1 = p1.Y;
         newLine.X2 = p2.X;
         newLine.Y2 = p2.Y;
         newLine.Stroke = brush;
         newLine.StrokeThickness = 1.5;
         newLine.IsHitTestVisible = false;
         return newLine;
      }

      public static List<GE_Primitive.PrimPoint> CreateCircle(GE_Primitive.PrimPoint center, double radius, int step)
      {
         List<GE_Primitive.PrimPoint> circlePoints = new();

         for (float a = 0; a <= 360; a += step)
         {
            circlePoints.Add(new GE_Primitive.PrimPoint(radius * Math.Cos(a * Math.PI / 180) + center.X,
                                                        radius * Math.Sin(a * Math.PI / 180) + center.Y));
         }

         return circlePoints;
      }

      public static bool IsBetween(double number, double left, double right)
      {
         return left <= number && number <= right;
      }
   }
}
