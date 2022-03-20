using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace GeoEditor
{
   public partial class App : Application
   {
      protected override void OnActivated(EventArgs e)
      {
         GE_ViewModel.DeskViewModel.Instance.SetScreen((System.Windows.Controls.Canvas)(MainWindow as View).DrawPlaceControl.MainRoot.Children[0]);
      }
   }
}
