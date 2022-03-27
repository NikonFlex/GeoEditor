namespace GE_ViewModel
{
   class Phantom
   {
      private GE_Primitive.PrimPolyline _primitive;
      private System.Windows.UIElement _phantomUI;

      public Phantom()
      {
         _primitive = new();
      }

      public void SetGeometry(GE_Primitive.PrimPolyline newGeometry)
      {
         _primitive = newGeometry;

         System.Windows.Shapes.Polyline polyline = _phantomUI as System.Windows.Shapes.Polyline;
         polyline.Points.Clear();

         foreach (GE_Primitive.PrimPoint point in _primitive.Points)
            polyline.Points.Add(new System.Windows.Point(point.X, point.Y));
      }

      public void Clear()
      {
         _primitive = new();
      }

      public System.Windows.UIElement CreateView()
      {

         return _phantomUI = GeoEditor.Utils.CreatePolyline(_primitive.Points, 1, System.Windows.Media.Brushes.LightGray);
      }
   }
}
