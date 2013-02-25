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
        public CheckoutStep2()
        {
            InitializeComponent();
        }

        private void HomeDelivery_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PickupInStore_Checked(object sender, RoutedEventArgs e)
        {
            if (StoreComboBox != null)
            {
                StoreComboBox.IsEnabled = true;
            }
        }

        private void BackButtonStep2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextStep2Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
