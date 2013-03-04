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
using Data.Desktop;

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
        private CheckoutStep4 step4;
        private LogInPage logIn;
        private DataHandler dataHandler;
        private ShoppingCart shoppingCart;


        public CheckoutWindow(DataHandler data)
        {
            shoppingCart = data.GetCart(); 
            InitializeComponent();
            wizardSteps.Background = new SolidColorBrush((Color)App.Current.Resources["DarkComplement"]);
            step1 = new CheckoutStep1(data);
            step1.NextStep += step1_NextStep;
            step1.LogIn += step1_LogIn;
            step2 = new CheckoutStep2(data);
            step2.NextStep2 += step2_NextStep2;
            step2.BackStep2 += step2_BackStep2;
            step3 = new CheckoutStep3(data);
            step3.NextStep3 += step3_NextStep3;
            step3.BackStep3 += step3_BackStep3;
            step4 = new CheckoutStep4();
            step4.ExitCheckout += step4_ExitCheckout;
            step4.BackStep4 += step4_BackStep4;
            step4.DataContext = data.GetCart();
            logIn = new LogInPage(data);
            logIn.NextStep += logIn_NextStep;
            logIn.BackStep += logIn_BackStep;
            if (data.GetUser() != null)
            {
                PageGrid.Children.Add(logIn);
            }
            else
            {
                PageGrid.Children.Add(step1);
            }
        }

        
        void step1_LogIn(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(logIn);
        }

        void logIn_BackStep(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step1);
        }

        void logIn_NextStep(object sender, EventArgs e)
        {

            ActivateStep2();
        }

        void step4_BackStep4(object sender, EventArgs e)
        {
            ActivateStep2();
        }

        void step3_NextStep3(object sender, EventArgs e)
        {
            ActivateStep4();
        }

        void step3_BackStep3(object sender, EventArgs e)
        {

            ActivateStep2();

        }

        void step4_ExitCheckout(object sender, EventArgs e)
        {
            this.Close();
        }

        void step2_BackStep2(object sender, EventArgs e)
        {
            ActivateStep1();
        }

        void step2_NextStep2(object sender, EventArgs e)
        {

            ActivateStep3();

        }

        void step1_NextStep(object sender, EventArgs e)
        {
            ActivateStep2();
        }
        
        public void ActivateStep1()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step1);
        }
        
        public void ActivateStep2()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step2);
        }

        private void ActivateStep3()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step3);
        }

        public void ActivateStep4()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step4);
            step4.displayContent();
            //if (step2.PayOnPickup.IsChecked == true)
            //{
            //    step3.chosenPaymentOption.Content = "Du betalar dina varor vid upphämtning.";
            //}
            //else
            //{
            //    step3.chosenPaymentOption.Content = "Du betalar med kort med nr: " + step2.CardNumberTextBox.Text;
            //}
            if (step2.HomeDelivery.IsChecked == true)
            {
                step4.chosenDeliveryOption.Content = "Dina varor kommer levereras hem till: " + step2.AddressTextBox.Text;
            }
            else
            {
                ComboBoxItem ci = (ComboBoxItem)step2.StoreComboBox.SelectedItem;
                string temp = ci.Content.ToString();
                step4.chosenDeliveryOption.Content = "Dina varor kommer levereras till: " + temp;
            }
        }
    }
}
