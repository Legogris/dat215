using Data;
using Data.Desktop;
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
    public partial class GridViewItem : ProductView
    {
        private AbstractSelector sel;

        override protected double NumberOfItems
        {
            get
            {
                return Item.Amount;
            }
        }

        public GridViewItem(ShoppingItem item) : base(item)
        {
            InitializeComponent();
            initContent();
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
