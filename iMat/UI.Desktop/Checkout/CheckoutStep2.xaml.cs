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
            PayWithCard.IsChecked = true;
            PayOnPickup.IsEnabled = false;
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
            if (cc != null && cc.Count > 0)
            {
                CardHolderNameTextBox.Text = cc.First().HoldersName;
                switch (cc.First().CardType) {
                    case CardType.Visa:
                        CardTypeComboBox.SelectedIndex = 0;
                        break;
                    case CardType.MasterCard:
                        CardTypeComboBox.SelectedIndex = 1;
                        break;
                    case CardType.Amex:
                        CardTypeComboBox.SelectedIndex = 2;
                        break;
                    default: throw new NotImplementedException();
                }
                CardNumberTextBox.Text = cc.First().CardNumber;
                MonthComboBox.SelectedValue = cc.First().ValidMonth - 1;
                YearComboBox.SelectedValue = cc.First().ValidYear - 13;
                VerificationCodeTextBox.Text = "" + cc.First().VerificationCode;
            }
        }

        private void PickupInStore_Checked(object sender, RoutedEventArgs e)
        {
            if (StoreComboBox != null && HomedeliveryGrid != null)
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
            if (rememberDetailsStep2 != null && rememberDetailsStep2.IsChecked == true)
            {
                if (sa.Count == 0)
                {
                    sa.Add(new ShippingAddress());
                }
                sa.First().Address = AddressTextBox.Text;
                sa.First().Email = EmailTextBox.Text;
                sa.First().FirstName = FirstNameTextBox.Text;
                sa.First().LastName = LastNameTextBox.Text;
                sa.First().PhoneNumber = PhoneNumberTextBox.Text;
                sa.First().PostAddress = PostAddressTextBox.Text;
                sa.First().PostCode = PostcodeTextBox.Text;
                if (cc.Count == 0)
                {
                    cc.Add(new CreditCard());
                }
                cc.First().VerificationCode = int.Parse(VerificationCodeTextBox.Text);
                cc.First().ValidYear = (int)MonthComboBox.SelectedIndex + 1;
                cc.First().ValidMonth = (int)YearComboBox.SelectedIndex + 13;
                cc.First().HoldersName = CardHolderNameTextBox.Text;
                switch (CardTypeComboBox.SelectedIndex) 
                {
                    case 0:
                        cc.First().CardType = CardType.Visa;
                        break;
                    case 1:
                        cc.First().CardType = CardType.MasterCard;
                        break;
                    case 2:
                        cc.First().CardType = CardType.Amex;
                        break;
                    default: throw new NotImplementedException();
                }
                cc.First().CardNumber = CardNumberTextBox.Text;

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

        private void agreeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (NextStep2Button != null)
            {
                NextStep2Button.IsEnabled = true;
            }
        }

        private void agreeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (NextStep2Button != null)
            {
                NextStep2Button.IsEnabled = false;
            }
        }
    }
}
