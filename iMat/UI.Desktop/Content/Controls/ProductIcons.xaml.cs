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
    /// Interaction logic for ProductIcons.xaml
    /// </summary>
    public partial class ProductIcons : UserControl
    {
        public ProductIcons()
        {
            InitializeComponent();
            this.DataContextChanged += ProductIcons_DataContextChanged;
        }

        void ProductIcons_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Product p = e.NewValue as Product;
            if (p != null)
            {
                kravIcon.Visibility = p.IsKRAV ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                fairTradeIcon.Visibility = p.IsFairTrade ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
        }
    }
}
