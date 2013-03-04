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

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutStep3.xaml
    /// </summary>
    public partial class CheckoutStep3 : UserControl
    {
        public event EventHandler NextStep3;
        public event EventHandler BackStep3;

        public CheckoutStep3(DataHandler data)
        {
            InitializeComponent();

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

        private void agreeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (NextStep2Button != null)
            //{
            //    NextStep2Button.IsEnabled = true;
            //}
        }

        private void agreeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (NextStep2Button != null)
            //{
            //    NextStep2Button.IsEnabled = false;
            //}
        }

        private void BackButtonStep3_Click(object sender, RoutedEventArgs e)
        {
            if (BackStep3 != null)
            {
                BackStep3.Invoke(this, null);
            }
        }

        private void NextStep3Button_Click(object sender, RoutedEventArgs e)
        {
            if (NextStep3 != null)
            {
                NextStep3.Invoke(this, null);
            }
        }

        private void MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
