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
    /// Interaction logic for BreadCrumbs.xaml
    /// </summary>
    public partial class BreadCrumbs : UserControl
    {
        FavoriteList list;
        public BreadCrumbs()
        {
            InitializeComponent();
            breadCrumb.ProductCategorySelected += breadCrumb_ProductCategorySelected;
            this.DataContextChanged += BreadCrumbs_DataContextChanged;
        }

        void BreadCrumbs_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            list = e.NewValue as FavoriteList;
            addListToCartButton.Visibility = list == null ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }

        void breadCrumb_ProductCategorySelected(object sender, ProductCategoryChangedEventArgs e)
        {
            if (ProductCategorySelected != null)
            {
                ProductCategorySelected.Invoke(sender, e);
            }
        }

        public event ProductCategoryChangedHandler ProductCategorySelected;
        public event ShoppingCartChangedHandler ItemAdded;

        private void addListToCartButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (list != null)
            {
                foreach (ShoppingItem item in list)
                {
                    if(ItemAdded != null) {
                        ItemAdded.Invoke(this, new CartEventArgs(CartEventArgs.CartEventType.Add, item));
                    }
                }
            }
        }
    }
}
