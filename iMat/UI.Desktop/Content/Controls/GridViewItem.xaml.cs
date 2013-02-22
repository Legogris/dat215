using Data;
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

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for GridViewItem.xaml
    /// </summary>
    public partial class GridViewItem : UserControl
    {
        private Product product;

        public GridViewItem(Product p)
        {
            InitializeComponent();
            product = p;
            initLabels();
        }

        private void initLabels() {
            productName.Content = product.Name;
        }

        private void addToShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
