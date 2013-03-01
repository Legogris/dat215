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
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : UserControl
    {

        public event EventHandler NextStep;
        public event EventHandler BackStep;
        private User user;

        public LogInPage(DataHandler data)
        {
            InitializeComponent();
            if (data.GetUser() != null)
            {
                user = data.GetUser();
                logInEmailTextBox.Text = user.Email;
            }
        }

        private void LogInNextButton_Click(object sender, RoutedEventArgs e)
        {
            NextStep.Invoke(this, null);
        }

        private void backToCreateButton_Click(object sender, RoutedEventArgs e)
        {
            BackStep.Invoke(this, null);
        }

        private void logInEmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkIfReadyToContinue();
        }

        private void logInPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkIfReadyToContinue();
        }
        private void checkIfReadyToContinue()
        {
            if (user != null && user.isPassword(logInPasswordBox.Password) && !(string.IsNullOrEmpty(logInEmailTextBox.Text) || string.IsNullOrEmpty(logInPasswordBox.Password)))
            {
                logInNextButton.IsEnabled = true;
                passwordWarningLabel.Content = null;
            }
            else
            {
                logInNextButton.IsEnabled = false;
                passwordWarningLabel.Content = "Lösenordet matchar inte din email";
            }
        }
    }
}
