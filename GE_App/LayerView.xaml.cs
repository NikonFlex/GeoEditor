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
   public partial class LayerView : UserControl
   {
      private int _layerID;
      private List<ObjectView> _objectViews = new();
      private bool _showObjects = false;

      public LayerView(int id)
      {
         InitializeComponent();
         LayerName.Content = "Layer " + $"{ id }";
         _layerID = id;
      }

      private void HideShowLayerButtonClick(object sender, RoutedEventArgs e)
      {
         GE_Model.Layer layer = GE_ViewModel.DeskViewModel.Instance.Model.Layers.Find(x => x.ID == _layerID);
         if (layer.IsVisible)
         {
            layer.SetVisible(false);
            ShowHideLayerButton.Content = "Show";
         }
         else
         {
            layer.SetVisible(true);
            ShowHideLayerButton.Content = "Hide";
         }
      }

      private void SelectButtonClick(object sender, RoutedEventArgs e)
      {

      }

      private void ShowHideObjectsButtonClick(object sender, RoutedEventArgs e)
      {
         if (!_showObjects)
         {
            _showObjects = true;
            GE_Model.Layer layer = GE_ViewModel.DeskViewModel.Instance.Model.Layers.Find(x => x.ID == _layerID);
            foreach (int objectID in layer.Objects)
            {
               _objectViews.Add(new(objectID));
               ObjectsConatiner.Children.Add(_objectViews.Last());
            }
            ShowHideObjectsButton.Content = "Hide";
         }
         else
         {
            _objectViews.Clear();
            ObjectsConatiner.Children.Clear();
            ShowHideObjectsButton.Content = "Show";
            _showObjects = false;
         }
      }

      private void LayerViewControlDoubleCLick(object sender, MouseButtonEventArgs e)
      {

      }
   }
}
