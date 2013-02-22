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
    /// Interaction logic for ListViewItem.xaml
    /// </summary>
    public partial class ListViewItem : UserControl
    {
        private Product product;

        public ListViewItem(Product p)
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
