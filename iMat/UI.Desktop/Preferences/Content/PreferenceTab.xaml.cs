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
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; toggleSelected(); }
        }

        public static readonly Brush BACKGROUND = (Brush)App.Current.Resources["ItemEvenBack"];
        public static readonly Brush HOVER = (Brush)App.Current.Resources["ItemOddBack"];
        public static readonly Brush SELECTED = (Brush)App.Current.Resources["DarkComplement"];
        
        public PreferenceTab(String name, BitmapImage image)
        {
            InitializeComponent();
            nameLabel.Content = name;
            imageLabel.Source = image;
        }

        private void toggleSelected()
        {
            background.Background = selected ? SELECTED : BACKGROUND;
        }

        private void BGMouseEnter(object sender, MouseEventArgs e)
        {
            if (selected) return;
            background.Background = HOVER;
        }

        private void BGMouseLeave(object sender, MouseEventArgs e)
        {
            toggleSelected();
        }
    }
}
