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
        public BreadCrumbs()
        {
            InitializeComponent();
            breadCrumb.ProductCategorySelected += breadCrumb_ProductCategorySelected;
        }

        void breadCrumb_ProductCategorySelected(UserControl sender, ProductCategoryChangedEventArgs e)
        {
            if (ProductCategorySelected != null)
            {
                ProductCategorySelected.Invoke(sender, e);
            }
        }

        public event ProductCategoryChangedHandler ProductCategorySelected;
    }
}
