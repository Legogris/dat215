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
            checkIfReadyToContinue();
            if (passwordWarningLabel.Content != null) return;
            NextStep.Invoke(this, null);
        }

        private void backToCreateButton_Click(object sender, RoutedEventArgs e)
        {
            BackStep.Invoke(this, null);
        }

        private void checkIfReadyToContinue()
        {
            if (user != null && user.isPassword(logInPasswordBox.Password) && !(string.IsNullOrEmpty(logInEmailTextBox.Text) || string.IsNullOrEmpty(logInPasswordBox.Password)))
            {
                passwordWarningLabel.Content = null;
            }
            else
            {
                passwordWarningLabel.Content = "Fel lösenord eller e-mail.";
            }
        }

        private void logInPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkIfReadyToContinue();
                if (passwordWarningLabel.Content != null) return;
                NextStep.Invoke(this, null);
            }
        }
    }
}
