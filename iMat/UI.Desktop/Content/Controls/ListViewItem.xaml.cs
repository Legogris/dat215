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
using Data;
using Data.Desktop;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ListViewItem.xaml
    /// </summary>
    public partial class ListViewItem : ProductView
    {
        public ListViewItem(ShoppingItem i) : base(i)
        {
            InitializeComponent();
            initContent();
        }        override protected double NumberOfItems
        {
            get
            {
                return Item.Amount;
            }
        }

        private void initContent() {
            productName.Content = Product.Name;
            if (Product.BoughtInBulk)
            {
                productPriceLabel.Content = Product.Price + " kr/st";
                productPriceLabel.Visibility = System.Windows.Visibility.Hidden;
                productJmfLabel.Content = Product.Price + string.Format(" {0}", Product.Unit);
            }
            else
            {
                productPriceLabel.Content = Product.Price + string.Format(" {0}", Product.Unit);
                productPriceLabel.Visibility = System.Windows.Visibility.Visible;
                productJmfLabel.Content = Product.ComparePrice + string.Format(" kr/l");
            }
            productImage.Source = ImageManager.GetImageForProduct(Product);
        }
    }
}