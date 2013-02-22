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

namespace UI.Desktop.Content.Controls
{
    /// <summary>
    /// Interaction logic for ShoppingCartItem.xaml
    /// </summary>
    public partial class ShoppingCartItem : UserControl
    {
        private ShoppingItem shoppingItem;

        public ShoppingCartItem(ShoppingItem shoppingItem)
        {
            InitializeComponent();
            this.shoppingItem = shoppingItem;
            nameLabel.Content = shoppingItem.Product.Name;
            amountTextBox.Text = shoppingItem.Amount + "";
            unitSuffixLabel.Content = shoppingItem.Product.UnitSuffix;
            costLabel.Content = shoppingItem.Total + AbstractSelector.CURRENCY;
        }
    }
}
