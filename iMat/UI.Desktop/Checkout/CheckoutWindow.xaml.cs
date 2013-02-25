using Data;
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
using System.Windows.Shapes;

namespace UI.Desktop.Checkout
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        private CheckoutStep1 step1;
        private CheckoutStep2 step2;
        private CheckoutStep3 step3;
        private ShoppingCart shoppingCart;


        public CheckoutWindow(ShoppingCart cart)
        {
            shoppingCart = cart; 
            InitializeComponent();
            step1 = new CheckoutStep1();
            step1.NextStep += step1_NextStep;
            step2 = new CheckoutStep2();
            step2.NextStep2 += step2_NextStep2;
            step2.BackStep2 += step2_BackStep2;
            step3 = new CheckoutStep3();
            step3.ExitCheckout += step3_ExitCheckout;
            step3.BackStep3 += step3_BackStep3;
            PageGrid.Children.Add(step1);
        }

        void step3_BackStep3(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step2);
        }

        void step3_ExitCheckout(object sender, EventArgs e)
        {
            this.Close();
        }

        void step2_BackStep2(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step1);
            Step1Indicator.BorderBrush = Brushes.Black;
            Step2Indicator.BorderBrush = Brushes.Transparent;
        }

        void step2_NextStep2(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step3);
        }

        void step1_NextStep(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step2);
            Step1Indicator.BorderBrush = Brushes.Transparent;
            Step2Indicator.BorderBrush = Brushes.Black;
        }

    }
}
