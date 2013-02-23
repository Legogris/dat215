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
                stackPanel.Children.Add(new ShoppingCartItem(e.ShoppingItem));
                updateTotalLabels();
            }
            else if (e.EventType == CartEventArgs.CartEventType.Change)
            {

            }
            else if (e.EventType == CartEventArgs.CartEventType.Clear)
            {

            }
            else if (e.EventType == CartEventArgs.CartEventType.Remove)
            {

            }
        }

        private void updateTotalLabels() {
            grandTotalLabel.Content = ShoppingCart.Total + " kr";
            itemsTotalLabel.Content = ShoppingCart.GetItems().Count + " st";
        }
    }
}
