using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GeoEditor
{
   class Constants
   {
      public static SolidColorBrush Black { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#000000");
      public static SolidColorBrush HoveredColor { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#01befe");
      public static SolidColorBrush SelectedColor { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#adff02");
      public static SolidColorBrush SnapToPointColor { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#38b000");
      public static SolidColorBrush SnapToLineColor { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#48bfe3");
      public static SolidColorBrush SnapToCenterColor { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#bb3e03");
      public static SolidColorBrush SnapToIntercectionColor { get; private set; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#38b000");
      public static double SnapDist { get; private set; } = 5;
   }
}
