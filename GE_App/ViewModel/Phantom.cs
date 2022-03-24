namespace GE_ViewModel
{
   class Phantom
   {
      public GE_Primitive.PrimPolyline Primitive { get; set; }

      public Phantom()
      {
         Primitive = new();
      }

      public void Clear()
      {
         Primitive = new();
      }

      public void Draw(System.Windows.Controls.Canvas screen)
      {
         for (int i = 0; i < Primitive.Points.Count - 1; i++)
            screen.Children.Add(GeoEditor.Utils.createSegmentLine(Primitive.Points[i], Primitive.Points[i + 1], 1, System.Windows.Media.Brushes.LightGray));
      }
   }
}
