using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeoEditor.Control
{
   public partial class ObjectView : UserControl
   {
      private int _objectID;

      public ObjectView(int id)
      {
         InitializeComponent();
         _objectID = id;
         ObjectName.Content = $"{ id }";
      }

      private void SelectButtonClick(object sender, RoutedEventArgs e)
      {

      }

      private void HideShowButtonClick(object sender, RoutedEventArgs e)
      {
         var objectScreenView = GE_ViewModel.DeskViewModel.Instance.ObjectsViews.FindObject(_objectID);
         if (objectScreenView.IsVisible)
         {
            objectScreenView.SetVisible(false);
            HideShowButton.Content = "Show";
         }
         else
         {
            objectScreenView.SetVisible(true);
            HideShowButton.Content = "Hide";
         }
      }
   }
}
