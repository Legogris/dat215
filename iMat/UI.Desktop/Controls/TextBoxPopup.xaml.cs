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

namespace UI.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxPopup.xaml
    /// </summary>
    public partial class TextBoxPopup : Window
    {
        public string TextBoxText
        {
            get;
            private set;
        }

        public TextBoxPopup()
        {
            InitializeComponent();
        }

        public void setFlavor(string s)
        {
            flavor.Content = s;
        }

        private void okButton_Click_1(object sender, RoutedEventArgs e)
        {
            TextBoxText = nameTextBox.Text;
            DialogResult = true;
        }
    }
}
