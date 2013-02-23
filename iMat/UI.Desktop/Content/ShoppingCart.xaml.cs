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

        public ShoppingCart ShoppingCart
        {
            get;
            set;
        }

        public ShoppingCartView()
        {
            InitializeComponent();                        
        }

        public void ShoppingCartChanged(Data.CartEventArgs e) {
            if (e.EventType == CartEventArgs.CartEventType.Add)
            {
                ShoppingCartItem si = new ShoppingCartItem(e.ShoppingItem);
                stackPanel.Children.Add(si);
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
                
            }
            updateTotalLabels();
        }

        private void updateTotalLabels() {
            grandTotalLabel.Content = ShoppingCart.Total + " kr";
            itemsTotalLabel.Content = ShoppingCart.GetItems().Count + " st";
        }

        private void clearShoppingCartButton_Click(object sender, RoutedEventArgs e)
        {
            ShoppingCart.Clear();
        }
    }
}
