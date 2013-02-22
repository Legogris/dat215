using Data.Desktop;
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

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataHandler dataHandler;
        public MainWindow()
        {
            InitializeComponent();
            initDataBinding();
        }
        
        private void initDataBinding() {
            dataHandler = DataHandler.ReadFromFile("asdf", "Resources/products.txt");
            leftMargin.RootCategory = dataHandler.GetRootCategory();
        }

        private void menuItemQuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToCheckout_Click(object sender, RoutedEventArgs e)
        {
            Checkout.CheckoutWindow temp = new Checkout.CheckoutWindow();
            temp.Show();
        }
    }
}
