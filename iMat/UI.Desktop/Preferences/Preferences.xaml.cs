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
using System.Windows.Shapes;
using UI.Desktop.Content;

namespace UI.Desktop.Preferences
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        private DataHandler dataHandler;

        public PreferencesWindow(DataHandler dh, UserControl open)
        {
            InitializeComponent();
            dataHandler = dh;
            content.Children.Add(open);
        }

        private void okClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
