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
using Data.Desktop;
using Data;

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutStep1.xaml
    /// </summary>
    public partial class CheckoutStep1 : UserControl
    {

        public event EventHandler NextStep;
        public event EventHandler LogIn;
        public DataHandler dataHandler;
        private User user;


        public CheckoutStep1(DataHandler data)
        {
            InitializeComponent();
            dataHandler = data;
            if (dataHandler.GetUser() != null)
            {
                user = dataHandler.GetUser();
                emailTextBox.Text = user.Email;
            }
        }

        private void NextStep1Button_Click(object sender, RoutedEventArgs e)
        {
            if (NextStep != null)
            {
                NextStep.Invoke(this, null);
            }
            if (rememberDetailsStep1 != null && rememberDetailsStep1.IsChecked == true)
            {
                dataHandler.setUser(emailTextBox.Text, mainPasswordBox.Password);
            }
        }

        private void LogInLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (LogIn != null)
            {
                LogIn.Invoke(this, null);
            }
        }

        private void CheckoutWithoutRegisterLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (NextStep != null)
            {
                NextStep.Invoke(this, null);
            }
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkAndEnable();
        }

        private void mainPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkAndEnable();
            if (mainPasswordBox.Password != repeatPasswordBox.Password)
            {
                passwordsNotEqualLabel.Content = "Lösenorden matchar inte!";
            }
            else
            {
                passwordsNotEqualLabel.Content = null;
            }
        }

        private void repeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkAndEnable();
        }

        private void checkAndEnable()
        {
            if (mainPasswordBox.Password == repeatPasswordBox.Password && !(string.IsNullOrEmpty(emailTextBox.Text) || string.IsNullOrEmpty(mainPasswordBox.Password) || string.IsNullOrEmpty(repeatPasswordBox.Password)))
            {
                if (user != null)
                {
                    NextStep1Button.IsEnabled = true;
                }
                else
                {
                    NextStep1Button.IsEnabled = true;
                }

            }
            else
            {
                NextStep1Button.IsEnabled = false;
            }
            if (mainPasswordBox.Password != repeatPasswordBox.Password)
            {
                passwordsNotEqualLabel.Content = "Lösenorden matchar inte!";
            }
            else
            {
                passwordsNotEqualLabel.Content = null;
            }
        }

    }
}
