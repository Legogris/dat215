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
        public ShoppingItem ShoppingItem
        {
            get; set;
        }

        public ShoppingCartItem(ShoppingItem shoppingItem)
        {
            InitializeComponent();
            ShoppingItem = shoppingItem;
            nameLabel.Content = shoppingItem.Product.Name;
            unitSuffixLabel.Content = shoppingItem.Product.UnitSuffix;
            updateAmount();
        }

        public void updateAmount() {
            amountTextBox.Text = ShoppingItem.Amount + "";
            costLabel.Content = ShoppingItem.Total + AbstractSelector.CURRENCY;
        }
    }
}
