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
         return newObject.ID;
      }
      public void RemoveObjectWithID(int id)
      {
         _objects.RemoveAll(obj => obj.ID == id);
      }
   }

   class GE_VM_ObjectsCollection
   {
      private List<GE_VMObject.VM_BaseObject> _objects = new();

      public IReadOnlyCollection<GE_VMObject.VM_BaseObject> ObjectsReadOnly => _objects;

      public int AddObject(GE_VMObject.VM_BaseObject newObject) // return id
      {
         _objects.Add(newObject);
         return newObject.ModelID;
      }
      public void RemoveObjectWithID(int id)
      {
         GE_Model.Model.Instance.RemoveObjectWithID(id);
         _objects.RemoveAll(view => view.ModelID == id);
      }
   }

   class GE_SelectionCollection
   {
      private List<int> _IDs = new();
      public IReadOnlyCollection<int> ObjectsReadOnly => _IDs;

      public void SelectObject(int id)
      {
         _IDs.Add(id);
      }

      public void DeSelectObject(int id)
      {
         _IDs.Remove(id);
      }

      public bool IsObjectSelected(int id)
      {
         return _IDs.Contains(id);
      }
   }

   static class Utils
   {
      public static Line CreateSegmentView(GE_Primitive.PrimPoint p1, GE_Primitive.PrimPoint p2, double thickness, SolidColorBrush brush)
      {
         Line newLine = new Line();
         newLine.X1 = p1.X;
         newLine.Y1 = p1.Y;
         newLine.X2 = p2.X;
         newLine.Y2 = p2.Y;
         newLine.Stroke = brush;
         newLine.StrokeThickness = thickness;
         newLine.IsHitTestVisible = false;
         return newLine;
      }

      public static Polygon CreatePolygon(List<GE_Primitive.PrimPoint> pointsList, double thickness, SolidColorBrush brush)
      {
         Polygon newPolygon = new Polygon();
         foreach (GE_Primitive.PrimPoint point in pointsList)
            newPolygon.Points.Add(new System.Windows.Point(point.X, point.Y));

         newPolygon.Stroke = brush;
         newPolygon.StrokeThickness = thickness;
         newPolygon.IsHitTestVisible = false;
         return newPolygon;
      }

      public static Polyline CreatePolyline(List<GE_Primitive.PrimPoint> pointsList, double thickness, SolidColorBrush brush)
      {
         Polyline newPolygon = new Polyline();
         foreach (GE_Primitive.PrimPoint point in pointsList)
            newPolygon.Points.Add(new System.Windows.Point(point.X, point.Y));

         newPolygon.Stroke = brush;
         newPolygon.StrokeThickness = thickness;
         newPolygon.IsHitTestVisible = false;
         return newPolygon;
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
