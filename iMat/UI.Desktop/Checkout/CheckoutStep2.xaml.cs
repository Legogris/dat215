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
        private List<CreditCard> cc;
        public CheckoutStep2(DataHandler data)
        {
            InitializeComponent();
            dataHandler = data;
        }

        private void HomeDelivery_Checked(object sender, RoutedEventArgs e)
        {
            HomedeliveryGrid.Visibility = Visibility.Visible;
            StoreComboBox.IsEnabled = false;
            
            sa = dataHandler.GetShippingAddresses();
            cc = dataHandler.GetCreditCards();
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
                StoreComboBox.IsEnabled = true;
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
            if (NextStep2 != null)
            {
                NextStep2.Invoke(this, null);
            }
        }

        private void PostcodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostAddressTextBox.Text = "Götefuckingborg";
        }

    }
}
