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
        public delegate void RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem);
        public event RemoveShoppingCartItem RemoveShoppingCartItemPress;
        public delegate void ItemAmountChanged(ShoppingCartItem shoppingCartItem, double amount);
        public event ItemAmountChanged ItemAmountChangedFromTextBox;

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

        private void mouseEnter(object sender, MouseEventArgs e)
        {
            removeButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            removeButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveShoppingCartItemPress.Invoke(this);
        }

        private void amountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                changeAmount();
            }
        }

        private void amountText_LostFocus(object sender, RoutedEventArgs e)
        {
            changeAmount();
        }

        private void changeAmount()
        {
            if (amountTextBox.Text.Length == 0) return;
            ItemAmountChangedFromTextBox.Invoke(this, Convert.ToDouble(amountTextBox.Text));
        }
    }
}
