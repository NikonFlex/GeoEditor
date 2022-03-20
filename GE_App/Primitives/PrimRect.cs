using System;
using System.Collections.Generic;
using System.Linq;
namespace GE_Primitive
{
   class PrimRect
   {
      private PrimPoint _origin;
      private double _width;
      private double _height;
     
      public PrimRect()
      {
         _origin = new();
         _width = 0;
         _height = 0;
      }

      public PrimRect(PrimPoint controlPoint, double width, double height)
      {
         _origin = controlPoint;
         _width = width;
         _height = height;
      }

      public double Left => _width > 0 ? _origin.X : _origin.X + _width;
      public double Right => _width > 0 ? _origin.X + _width : _origin.X;
      public double Top => _height > 0 ? _origin.Y : _origin.Y + _height;
      public double Bottom => _height > 0 ? _origin.Y + _height : _origin.Y;
      public PrimPoint ControlPoint => _origin;

      public void SetWidth(double newWidth)
      {
         _width = newWidth;
      }

      public void SetHeight(double newHeight)
      {
         _height = newHeight;
      }

      public bool IsInside(PrimPoint point)
      {
         return Left <= point.X && point.X <= Right && Top <= point.Y && point.Y <= Bottom;
      }
   }
}
