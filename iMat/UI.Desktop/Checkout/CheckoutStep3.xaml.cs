﻿using System;
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
using Data;

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutStep3.xaml
    /// </summary>
    public partial class CheckoutStep3 : UserControl
    {

        public event EventHandler ExitCheckout;
        public event EventHandler BackStep3;

        public IList<ShoppingItem> ShoppingItemsCollection
        {
            get { 
                ShoppingCart cart = DataContext as ShoppingCart;
                return cart.GetItems();
            }
        }

        public CheckoutStep3()
        {
            InitializeComponent();
            
        }

        public void displayContent()
        {
            checkoutListView.ItemsSource = ShoppingItemsCollection;
        }

        private void exitCheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExitCheckout != null)
            {
                ExitCheckout.Invoke(this, null);
            }
        }
        private void backStep3Button_Click(object sender, RoutedEventArgs e)
        {
            if (BackStep3 != null)
            {
                BackStep3.Invoke(this, null);
            }
        }
    }
}
