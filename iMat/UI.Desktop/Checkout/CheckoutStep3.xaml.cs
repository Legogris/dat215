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
using Data;

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutStep3.xaml
    /// </summary>
    public partial class CheckoutStep3 : UserControl
    {
        public event EventHandler NextStep3;
        public event EventHandler BackStep3;
        private List<CreditCard> cc;

        public CheckoutStep3(DataHandler data)
        {
            InitializeComponent();
            cc = data.GetCreditCards();
        }

        private void PayWithCard_Checked(object sender, RoutedEventArgs e)
        {
            if (PaymentGrid != null)
            {
                PaymentGrid.Visibility = Visibility.Visible;
            }
            if (cc != null && cc.Count > 0)
            {
                CardHolderNameTextBox.Text = cc.First().HoldersName;
                CardNumberTextBox.Text = cc.First().CardNumber;
                switch (cc.First().CardType)
                {
                    case CardType.Amex:
                        CardTypeComboBox.SelectedIndex = 0;
                        break;
                    case CardType.MasterCard:
                        CardTypeComboBox.SelectedIndex = 1;
                        break;
                    case CardType.Visa:
                        CardTypeComboBox.SelectedIndex = 2;
                        break;
                    default: throw new NotImplementedException();
                }
                MonthComboBox.SelectedIndex = cc.First().ValidMonth;
                YearComboBox.SelectedIndex = cc.First().ValidYear;
                VerificationCodeTextBox.Text = cc.First().VerificationCode.ToString();
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
            if (NextStep3Button != null)
            {
                NextStep3Button.IsEnabled = true;
            }
        }

        private void agreeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (NextStep3Button != null)
            {
                NextStep3Button.IsEnabled = false;
            }
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
            if (rememberDetailsStep2.IsChecked.Value)
            {
                if (cc.Count == 0)
                {
                    cc.Add(new CreditCard());
                }
                cc.First().HoldersName = CardHolderNameTextBox.Text;
                cc.First().CardNumber = CardNumberTextBox.Text;
                ComboBoxItem temp = (ComboBoxItem)CardTypeComboBox.SelectedItem;
                switch (temp.Content.ToString()) 
                {
                    case "Amex": 
                        cc.First().CardType = CardType.Amex;
                        break;
                    case "MasterCard": 
                        cc.First().CardType = CardType.MasterCard;
                        break;
                    case "Visa":
                        cc.First().CardType = CardType.Visa;
                        break;
                    default: throw new NotImplementedException();
                }
                cc.First().ValidMonth = MonthComboBox.SelectedIndex;
                cc.First().ValidYear = YearComboBox.SelectedIndex;
                cc.First().VerificationCode = int.Parse(VerificationCodeTextBox.Text);
            }
            if (NextStep3 != null)
            {
                NextStep3.Invoke(this, null);
            }
        }

        private void CardNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckoutWindow.isTextAllowed(e.Text);
        }

        private void VerificationCodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckoutWindow.isTextAllowed(e.Text);
        }
    }
}
