﻿using Data;
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
using UI.Desktop.Preferences;
using System.Text.RegularExpressions;

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
        private CompletedCheckoutPage step5;
        private LogInPage logIn;
        private ShoppingCart shoppingCart;
        private DataHandler dataHandler;

        public CheckoutWindow(DataHandler data)
        {
            dataHandler = data;
            shoppingCart = data.GetCart(); 
            InitializeComponent();
            wizardSteps.Background = (Brush)App.Current.Resources["DarkComplement"];
            initWizSteps(wStep1, "Konto", "UserImage");
            initWizSteps(wStep2, "Frakt", "ShipmentImage");
            initWizSteps(wStep3, "Betalning", "PaymentImage");
            initWizSteps(wStep4, "Bekräfta", "ConfirmImage");
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
            step5 = new CompletedCheckoutPage();
            step5.ExitWizard += step5_ExitWizard;
            step5.ShowReciept += step5_ShowReciept;
            if (data.GetUser() != null)
            {
                PageGrid.Children.Add(step2);
                logInUser.Content = data.GetUser().Email;
                logInStatus.Content = "Inloggad som: ";
                wStep2.StepActive = true;
            }
            else
            {
                PageGrid.Children.Add(step1);
                logInStatus.Content = "Inte inloggad!";
                wStep1.StepActive = true;
            }
            

        }
        public static bool isTextAllowed(String s)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(s);
        }

        void step5_ShowReciept(object sender, EventArgs e)
        {
            this.Close();
            PreferencesWindow pref = new PreferencesWindow(dataHandler, PreferencesWindow.StartupView.Account);
            pref.Owner = MainWindow.WindowContainer;
            pref.ShowDialog();
        }

        void step5_ExitWizard(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initWizSteps(WizardSteps wStep, String title, String img)
        {
            wStep.Title = title;
            wStep.ImgSource = img;
            wStep.StepActive = false;
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
            wStep2.StepActive = true;
            wStep1.StepActive = false;
        }

        void step4_BackStep4(object sender, EventArgs e)
        {
            ActivateStep3();
            wStep3.StepActive = true;
            wStep4.StepActive = false;
        }

        void step3_NextStep3(object sender, EventArgs e)
        {
            ActivateStep4();
            wStep4.StepActive = true;
            wStep3.StepActive = false;
        }

        void step3_BackStep3(object sender, EventArgs e)
        {
            ActivateStep2();
            wStep2.StepActive = true;
            wStep3.StepActive = false;
        }

        void step4_ExitCheckout(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step5);
            wStep4.StepActive = false;
            if (shoppingCart != null)
            {
                dataHandler.GetOrders().Insert(0, new Order(shoppingCart, DateTime.Now, 1337));
                shoppingCart.Clear();
            }
        }

        void step2_BackStep2(object sender, EventArgs e)
        {
            ActivateStep1();
            wStep1.StepActive = true;
            wStep2.StepActive = false;
        }

        void step2_NextStep2(object sender, EventArgs e)
        {
            ActivateStep3();
            wStep3.StepActive = true;
            wStep2.StepActive = false;
        }

        void step1_NextStep(object sender, EventArgs e)
        {
            ActivateStep2();
            wStep2.StepActive = true;
            wStep1.StepActive = false;
            if (dataHandler.GetUser() != null)
            {
                logInUser.Content = dataHandler.GetUser().Email;
                logInStatus.Content = "Inloggad som: ";
            }
            else
            {
                logInStatus.Content = "Inte inloggad!";
            }
        }
        
        public void ActivateStep1()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step1 = new CheckoutStep1(dataHandler));
            step1.NextStep += step1_NextStep;
            step1.LogIn += step1_LogIn;
        }
        
        public void ActivateStep2()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step2);
            if (dataHandler.GetUser() != null)
            {
                step2.EmailTextBox.Text = dataHandler.GetUser().ShippingAddress.Email;
            }
        }

        private void ActivateStep3()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step3);
            if (step2.PickupInStore.IsChecked == true)
            {
                step3.PayOnPickup.IsEnabled = true;
            }
            else
            {
                step3.PayOnPickup.IsEnabled = false;
            }
            step3.PayWithCard.IsChecked = true;
        }

        public void ActivateStep4()
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step4);
            step4.displayContent();
            if (step3.PayOnPickup.IsChecked == true)
            {
                step4.chosenPaymentOption.Text = "Du betalar dina varor vid upphämtning.";
                step4.chosenPaymentAnswer.Text = null;
            }
            else
            {
                step4.chosenPaymentOption.Text = "Du betalar med kort med nr: ";
                step4.chosenPaymentAnswer.Text = step3.CardNumberTextBox.Text;
            }
            if (step2.HomeDelivery.IsChecked == true)
            {
                step4.chosenDeliveryOption.Text = "Dina varor kommer levereras hem till: ";
                step4.chosenDeliveryAnswer.Text = step2.AddressTextBox.Text;
            }
            else
            {
                ComboBoxItem ci = (ComboBoxItem)step2.StoreComboBox.SelectedItem;
                string temp = ci.Content.ToString();
                step4.chosenDeliveryOption.Text = "Dina varor kommer levereras till: ";
                step4.chosenDeliveryAnswer.Text = temp;
            }
        }
    }
}
