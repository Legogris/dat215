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
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : UserControl
    {

        public event EventHandler NextStep;
        public event EventHandler BackStep;

        public LogInPage()
        {
            InitializeComponent();
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
            if (!(string.IsNullOrEmpty(logInEmailTextBox.Text) || string.IsNullOrEmpty(logInPasswordBox.Password)))
            {
                logInNextButton.IsEnabled = true;
            }
            else
            {
                logInNextButton.IsEnabled = false;
            }
        }
    }
}
