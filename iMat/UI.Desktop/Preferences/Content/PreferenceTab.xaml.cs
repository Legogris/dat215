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

namespace UI.Desktop.Preferences.Content
{
    /// <summary>
    /// Interaction logic for PreferenceTab.xaml
    /// </summary>
    public partial class PreferenceTab : UserControl
    {
        public PreferenceTab(String name, BitmapImage image)
        {
            InitializeComponent();
            nameLabel.Content = name;
            imageLabel.Source = image;
        }

        private void BGMouseEnter(object sender, MouseEventArgs e)
        {
            background.Background = new SolidColorBrush((Color)App.Current.Resources["ItemHighlight"]);
        }

        private void BGMouseLeave(object sender, MouseEventArgs e)
        {
            background.Background = new SolidColorBrush(Colors.White);
        }
    }
}
