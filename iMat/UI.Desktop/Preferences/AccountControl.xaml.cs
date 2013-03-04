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

namespace UI.Desktop.Preferences
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        private DataHandler dataHandler;

        public AccountControl(DataHandler dh)
        {
            InitializeComponent();
            dataHandler = dh;
            labels();
        }

        private void labels()
        {
            accountname.Content = dataHandler.GetUser().Email;
            ShippingAddress sa = dataHandler.GetShippingAddresses().First();
            forename.Text = sa.FirstName;
            lastname.Text = sa.LastName;
            email.Text = sa.Email;
            street.Text = sa.Address;
            postcode.Text = sa.PostCode;
            phone.Text = sa.PhoneNumber;
        }

        public void save()
        {
            // TODO: save that shit
        }
    }
}
