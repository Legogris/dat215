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


        public CheckoutWindow()
        {
            InitializeComponent();
            step1 = new CheckoutStep1();
            step1.NextStep += step1_NextStep;
            step2 = new CheckoutStep2();
            step2.NextStep2 += step2_NextStep2;
            step2.BackStep2 += step2_BackStep2;
            PageGrid.Children.Add(step1);
        }

        void step2_BackStep2(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step1);
        }

        void step2_NextStep2(object sender, EventArgs e)
        {
            //PageGrid.Children.Clear();
            //PageGrid.Children.Add
        }

        void step1_NextStep(object sender, EventArgs e)
        {
            PageGrid.Children.Clear();
            PageGrid.Children.Add(step2);
        }

    }
}
