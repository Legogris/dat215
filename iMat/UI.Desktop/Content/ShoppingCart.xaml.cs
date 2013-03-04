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
using Data.Desktop;

namespace UI.Desktop.Content
{
    /// <summary>
    /// Interaction logic for ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCartView : UserControl
    {
        private DataHandler dataHandler;
        private Label notificationOldShoppingCart;
        public ShoppingCart ShoppingCart
        {
            get { return dataHandler.GetCart(); }
        }
        public DataHandler DataHandler
        {
            get { return dataHandler; }
            set { dataHandler = value; updateShoppingCartItems(); }
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
            if (ShoppingCart.GetItems().Count != 0)
            {
                notificationOldShoppingCart = new Label();
                notificationOldShoppingCart.Content = "Din kundvagn från förra sessionen:";
                notificationOldShoppingCart.FontWeight = FontWeights.Bold;
                stackPanel.Children.Add(notificationOldShoppingCart);
            }
            foreach (ShoppingItem cartItem in this.ShoppingCart.GetItems())
            {
                addShoppingCartItem(cartItem);
            }

            
            updateTotalLabels();
        }

        // absolut största clusterfucket i mänsklighetens historia. men jag bryr mig inte.
        public void ShoppingCartChanged(Data.CartEventArgs e) {
            if (stackPanel.Children.Count != 0 && stackPanel.Children[0] == notificationOldShoppingCart)
            {
                stackPanel.Children.RemoveAt(0);
            }
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
            addShoppingCartToListButton.ContextMenu.PlacementTarget = addShoppingCartToListButton;
            addShoppingCartToListButton.DataContext = ShoppingCart.GetItems();
            e.Handled = true;
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            Checkout.CheckoutWindow wizard = new Checkout.CheckoutWindow(DataHandler);
            wizard.Owner = MainWindow.WindowContainer;
            wizard.ShowDialog();
        }
    }
}
