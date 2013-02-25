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
using UI.Desktop.Content.Controls;

namespace UI.Desktop.Content
{
    /// <summary>
    /// Interaction logic for ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCartView : UserControl
    {
        private ShoppingCart shoppingCart;

        public ShoppingCart ShoppingCart
        {
            get { return shoppingCart; }
            set { shoppingCart = value; updateShoppingCartItems(); }
        }

        public void SetListContextMenu(ContextMenu menu)
        {
            addShoppingCartToListButton.ContextMenu = menu;
        }

        public ShoppingCartView()
        {
            InitializeComponent();
        }

        private void updateShoppingCartItems()
        {
            stackPanel.Children.Clear();
            foreach (ShoppingItem cartItem in this.ShoppingCart.GetItems())
            {
                addShoppingCartItem(cartItem);
            }

            // TODO: modeless feedback som berättar att saker har lagts till om ShoppingCart.GetItems() != empty?

            updateTotalLabels();
        }

        // absolut största clusterfucket i mänsklighetens historia. men jag bryr mig inte.
        public void ShoppingCartChanged(Data.CartEventArgs e) {
            if (e.EventType == CartEventArgs.CartEventType.Add)
            {
                addShoppingCartItem(e.ShoppingItem);
            }
            else if (e.EventType == CartEventArgs.CartEventType.Change)
            {
                foreach (ShoppingCartItem sci in stackPanel.Children)
                {
                    if (sci.ShoppingItem == e.ShoppingItem) {
                        sci.updateAmount();
                    }
                }
            }
            else if (e.EventType == CartEventArgs.CartEventType.Clear)
            {
                stackPanel.Children.Clear();
            }
            else if (e.EventType == CartEventArgs.CartEventType.Remove)
            {
                ShoppingCartItem toRemove = null;
                foreach (ShoppingCartItem sci in stackPanel.Children)
                {
                    if (sci.ShoppingItem == e.ShoppingItem)
                    {
                        toRemove = sci;
                    }
                }
                if (toRemove != null)
                {
                    stackPanel.Children.Remove(toRemove);
                }
            }
            updateTotalLabels();
        }

        private void addShoppingCartItem(ShoppingItem item)
        {
            ShoppingCartItem si = new ShoppingCartItem(item);
            si.RemoveShoppingCartItemPress += ShoppingCartItem_RemoveShoppingCartItemPress;
            si.ItemAmountChangedFromTextBox += ShoppingCartItem_ItemAmountChangedFromTextBox;
            stackPanel.Children.Add(si);
        }

        void ShoppingCartItem_ItemAmountChangedFromTextBox(ShoppingCartItem shoppingCartItem, double d)
        {
            ShoppingCart.Change(shoppingCartItem.ShoppingItem, d);
        }

        void ShoppingCartItem_RemoveShoppingCartItemPress(ShoppingCartItem shoppingCartItem)
        {
            ShoppingCart.Remove(shoppingCartItem.ShoppingItem);
        }

        private void updateTotalLabels() {
            grandTotalLabel.Content = ShoppingCart.Total + " kr";
            itemsTotalLabel.Content = ShoppingCart.GetItems().Count + " st";
        }

        private void clearShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            ShoppingCart.Clear();
        }

        private void addShoppingCartToListButton_Click(object sender, RoutedEventArgs e)
        {
            addShoppingCartToListButton.ContextMenu.IsOpen = true;
            e.Handled = true;
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            Checkout.CheckoutWindow wizard = new Checkout.CheckoutWindow();
            wizard.Show();
        }
    }
}
