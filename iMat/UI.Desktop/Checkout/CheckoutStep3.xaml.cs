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
        public CheckoutStep3()
        {
            InitializeComponent();
        }

        private void exitCheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExitCheckout != null)
            {
                ExitCheckout.Invoke(this, null);
            }
        }
        private void backStep3Button_Click(object sender, RoutedEventArgs e)
        {
            if (BackStep3 != null)
            {
                BackStep3.Invoke(this, null);
            }
        }

        public event EventHandler ExitCheckout;
        public event EventHandler BackStep3;

    }
}
