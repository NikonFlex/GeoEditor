using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using GE_Primitive;

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

      public GE_GeomObject.BaseObject FindObject(int id)
      {
         return _objects.Find(obj => obj.ID == id); 
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
         GE_ViewModel.DeskViewModel.Instance.Model.RemoveObjectWithID(id);
         _objects.RemoveAll(view => view.ModelID == id);
      }

      public GE_VMObject.VM_BaseObject FindObject(int id)
      {
         return _objects.Find(obj => obj.ModelID == id);
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
      public static Line CreateSegmentView(PrimPoint p1, PrimPoint p2, double thickness, SolidColorBrush brush)
      {
         Line newLine = new();
         newLine.X1 = p1.X;
         newLine.Y1 = p1.Y;
         newLine.X2 = p2.X;
         newLine.Y2 = p2.Y;
         newLine.Stroke = brush;
         newLine.StrokeThickness = thickness;
         newLine.IsHitTestVisible = false;
         return newLine;
      }

      public static Polygon CreatePolygon(List<PrimPoint> pointsList, double thickness, SolidColorBrush strokeBrush, SolidColorBrush fillBrush = null)
      {
         Polygon newPolygon = new();
         foreach (PrimPoint point in pointsList)
            newPolygon.Points.Add(new System.Windows.Point(point.X, point.Y));

         newPolygon.Stroke = strokeBrush;
         newPolygon.StrokeThickness = thickness;
         newPolygon.IsHitTestVisible = false;
         newPolygon.Fill = fillBrush;
         return newPolygon;
      }

      public static Polyline CreatePolyline(List<PrimPoint> pointsList, double thickness, SolidColorBrush brush)
      {
         Polyline newPolyline = new();
         foreach (PrimPoint point in pointsList)
            newPolyline.Points.Add(new System.Windows.Point(point.X, point.Y));

         newPolyline.Stroke = brush;
         newPolyline.StrokeThickness = thickness;
         newPolyline.IsHitTestVisible = false;
         return newPolyline;
      }

      public static List<PrimPoint> CreateRegularFigure(PrimPoint center, double radius, int step)
      {
         List<PrimPoint> circlePoints = new();

         for (float a = 0; a <= 360; a += step)
         {
            circlePoints.Add(new PrimPoint(radius * Math.Cos(a * Math.PI / 180) + center.X,
                                           radius * Math.Sin(a * Math.PI / 180) + center.Y));
         }

         return circlePoints;
      }

      public static TextBlock CreateTextBlock(PrimPoint textPos, string text, SolidColorBrush textBrush, SolidColorBrush backgroundBrush = null)
      {
         TextBlock textBlock = new();
         textBlock.Text = text;
         textBlock.Foreground = textBrush;
         textBlock.Background = backgroundBrush;
         Canvas.SetLeft(textBlock, textPos.X);
         Canvas.SetTop(textBlock, textPos.Y);
         return textBlock;
      }

      public static bool IsBetween(double number, double left, double right)
      {
         return left <= number && number <= right;
      }
   }
}
