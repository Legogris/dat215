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
        private AbstractSelector sel;

        public Product Product { get { return product; } }

        public GridViewItem(Product p)
        {
            InitializeComponent();
            product = p;
            initContent();
        }

        private void initContent() {
            productName.Content = product.Name;
            sel = Product.BoughtInBulk ? (AbstractSelector)new BulkSelector(Product) : new ItemSelector(Product);
            sel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            sel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            selectorContainer.Children.Add(sel);
        }

        private void addToShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemAdded != null)
            {
                ItemAdded.Invoke(this, new CartEventArgs(CartEventArgs.CartEventType.Add, new ShoppingItem(product, sel.NumberOfItems)));
            }
        }

        public event Data.ShoppingCartChangedHandler ItemAdded;
    }
}
