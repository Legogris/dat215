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

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutStep2.xaml
    /// </summary>
    public partial class CheckoutStep2 : UserControl
    {
        public event EventHandler NextStep2;
        public event EventHandler BackStep2;
        public CheckoutStep2()
        {
            InitializeComponent();
        }

        private void HomeDelivery_Checked(object sender, RoutedEventArgs e)
        {
            HomedeliveryGrid.Visibility = Visibility.Visible;
            StoreComboBox.IsEnabled = false;
            PayOnPickup.IsEnabled = true;
        }

        private void PickupInStore_Checked(object sender, RoutedEventArgs e)
        {
            if (StoreComboBox != null)
            {
                HomedeliveryGrid.Visibility = Visibility.Collapsed;
                StoreComboBox.IsEnabled = true;
                PayOnPickup.IsEnabled = true;
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

        private void PayWithCard_Checked(object sender, RoutedEventArgs e)
        {
            if (PaymentGrid != null)
            {
                PaymentGrid.Visibility = Visibility.Visible;
            }
        }

        private void PayOnPickup_Checked(object sender, RoutedEventArgs e)
        {
            if (PaymentGrid != null)
            {
                PaymentGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
