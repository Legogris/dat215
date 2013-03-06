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
    /// Interaction logic for CompletedCheckoutPage.xaml
    /// </summary>
    public partial class CompletedCheckoutPage : UserControl
    {
        public event EventHandler ShowReciept;
        public event EventHandler ExitWizard;
        public CompletedCheckoutPage()
        {
            InitializeComponent();
        }

        private void showRecieptButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowReciept != null)
            {
                ShowReciept.Invoke(this, null);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExitWizard != null)
            {
                ExitWizard.Invoke(this, null);
            }
        }
    }
}
