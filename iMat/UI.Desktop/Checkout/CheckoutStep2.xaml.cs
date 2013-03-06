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
using Data.Desktop;

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutStep2.xaml
    /// </summary>
    public partial class CheckoutStep2 : UserControl
    {
        private DataHandler dataHandler;
        public event EventHandler NextStep2;
        public event EventHandler BackStep2;
        private List<ShippingAddress> sa;
        public CheckoutStep2(DataHandler data)
        {
            InitializeComponent();
            dataHandler = data;
        }

        private void HomeDelivery_Checked(object sender, RoutedEventArgs e)
        {
            HomedeliveryGrid.Visibility = Visibility.Visible;
            StoreComboBox.Visibility = Visibility.Collapsed;
            
            sa = dataHandler.GetShippingAddresses();
            if (sa != null && sa.Count > 0 )
            {
                FirstNameTextBox.Text = sa.First().FirstName;
                LastNameTextBox.Text = sa.First().LastName;
                AddressTextBox.Text = sa.First().Address;
                PostAddressTextBox.Text = sa.First().PostAddress;
                PostcodeTextBox.Text = sa.First().PostCode;
                PhoneNumberTextBox.Text = sa.First().PhoneNumber;
                EmailTextBox.Text = sa.First().Email;
            }            
        }

        private void PickupInStore_Checked(object sender, RoutedEventArgs e)
        {
            if (StoreComboBox != null && HomedeliveryGrid != null)
            {
                HomedeliveryGrid.Visibility = Visibility.Collapsed;
                StoreComboBox.Visibility = Visibility.Visible;
            }
        }

        private void BackButtonStep2_Click(object sender, RoutedEventArgs e)
        {
            if (BackStep2 != null)
            {
                BackStep2.Invoke(this, null);
            }
        }

        private void NextStep2Button_Click(object sender, RoutedEventArgs e)
        {
            if (HomeDelivery.IsChecked.Value)
            {
                if (sa.Count == 0)
                {
                    sa.Add(new ShippingAddress());
                }
                sa.First().FirstName = FirstNameTextBox.Text;
                sa.First().LastName = LastNameTextBox.Text;
                sa.First().Address = AddressTextBox.Text;
                sa.First().PostAddress = PostAddressTextBox.Text;
                sa.First().PostCode = PostcodeTextBox.Text;
                sa.First().PhoneNumber = PhoneNumberTextBox.Text;
                sa.First().Email = EmailTextBox.Text;
            }
            if (NextStep2 != null)
            {
                NextStep2.Invoke(this, null);
            }            
        }

        private void PostcodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostAddressTextBox.Text = "Göteborg";
        }

        private void PhoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckoutWindow.isTextAllowed(e.Text);
        }

        private void PostcodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckoutWindow.isTextAllowed(e.Text);
        }

    }
}
