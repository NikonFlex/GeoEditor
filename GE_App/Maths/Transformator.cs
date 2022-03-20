namespace GE_Maths
{
   class Transformator
   {
      public double Scale { get; set; }
      public GE_Primitive.PrimPoint Pivot { get; set; } //центр экрана в мировых координатах
      public double ScreenWidth { get; private set; }
      public double ScreenHeight { get; private set; }

      public Transformator(double screenWidth, double screenHeight)
      {
         ScreenWidth = screenWidth;
         ScreenHeight = screenHeight;
         Scale = 1;
         Pivot = new();
      }

      public GE_Primitive.PrimPoint ScreenToWorld(GE_Primitive.PrimPoint screenPoint)
      {
         double globalX = Pivot.X + (screenPoint.X - ScreenWidth / 2.0f) * Scale;
         double globalY = Pivot.Y - (screenPoint.Y - ScreenHeight / 2.0f) * Scale;
         return new(globalX, globalY);
      }

      public GE_Primitive.PrimPoint WorldToScreen(GE_Primitive.PrimPoint globalPoint)
      {
         double screenX = ScreenWidth / 2 + (globalPoint.X - Pivot.X) / Scale;
         double screenY = ScreenHeight / 2 - (globalPoint.Y - Pivot.Y) / Scale;
         return new(screenX, screenY);
      }
   }
}
