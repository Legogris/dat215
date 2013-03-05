﻿using Data;
using Data.Desktop;
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
using UI.Desktop.Preferences.Content;

namespace UI.Desktop.Preferences
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        private DataHandler dataHandler;
        private ShippingAddress sa;

        public IList<Order> OrderCollection
        {
            get { return dataHandler.GetOrders(); }
        }

        public AccountControl(DataHandler dh)
        {
            dataHandler = dh;
            InitializeComponent();
            initOverview();
            if (dataHandler.GetUser() == null)
            {
                accountLogIn.Visibility = System.Windows.Visibility.Visible;
                accountStatus.Content = "Inte inloggad";
            }
            else
            {
                accountInfo.Visibility = System.Windows.Visibility.Visible;
                if (dataHandler.GetShippingAddresses().Count != 0)
                {
                    sa = dataHandler.GetShippingAddresses().First();
                    labels();
                } 
            }
        }

        private void labels()
        {
            accountname.Content = dataHandler.GetUser().Email;
            forename.Text = sa.FirstName;
            lastname.Text = sa.LastName;
            email.Text = sa.Email;
            street.Text = sa.Address;
            postcode.Text = sa.PostCode;
            phone.Text = sa.PhoneNumber;
        }

        public void save()
        {
            if (dataHandler.GetUser() != null)
            {
                if (sa == null)
                {
                    dataHandler.GetShippingAddresses().Add(new ShippingAddress());
                    sa = dataHandler.GetShippingAddresses().First();
                }
                sa.FirstName = forename.Text;
                sa.LastName = lastname.Text;
                sa.Address = street.Text;
                sa.PostAddress = "Göteborg";
                sa.PostCode = postcode.Text;
                sa.PhoneNumber = phone.Text;
                sa.Email = email.Text;
            }
        }

        private void logIn(object sender, RoutedEventArgs e)
        {
            dataHandler.setUser(logInAccName.Text, logInPW.Text);
            logIn();
        }

        private void goToCreateAccount(object sender, RoutedEventArgs e)
        {
            accountLogIn.Visibility = System.Windows.Visibility.Collapsed;
            accountCreate.Visibility = System.Windows.Visibility.Visible;
        }

        private void createAccount(object sender, RoutedEventArgs e)
        {
            dataHandler.setUser(createAccName.Text, createPW.Text);
            logIn();
        }

        private void logIn() {
            accountInfo.Visibility = System.Windows.Visibility.Visible;
            accountLogIn.Visibility = System.Windows.Visibility.Collapsed;
            accountCreate.Visibility = System.Windows.Visibility.Collapsed;
            accountname.Content = dataHandler.GetUser().Email;
            accountStatus.Content = "Inloggad som";
            if (dataHandler.GetShippingAddresses().Count != 0)
            {
                labels();
            }
        }

        private void initOverview()
        {
            orderHeader.fulhack();
            IList<Order> orders = dataHandler.GetOrders();
            foreach (Order order in orders)
            {
                AbstractOrderItem item = new AbstractOrderItem();
                overview.Children.Add(item);
                item.Date(order.Date.ToString());
                item.Order(order.OrderNumber.ToString());
                item.Total(order.TotalCost.ToString());
            }
        }
    }
}
