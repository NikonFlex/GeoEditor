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
   public partial class LayersControl : UserControl
   {
      private List<LayerView> _layersUI = new();

      public LayersControl()
      {
         InitializeComponent();
         createNewLayer();
      }

      private void AddLayerButtonClick(object sender, RoutedEventArgs e)
      {
         createNewLayer();
      }

      private void createNewLayer()
      {
         _layersUI.Add(new(GE_ViewModel.DeskViewModel.Instance.Model.AddLayer()));
         LayersList.Children.Add(_layersUI.Last());
      }
   }
}
