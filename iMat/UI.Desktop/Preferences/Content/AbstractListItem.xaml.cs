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
    /// Interaction logic for AbstractOrderItem.xaml
    /// </summary>
    public partial class AbstractListItem : UserControl
    {
        public static readonly Brush WHITE = new SolidColorBrush(Colors.White);
        public static readonly Brush HOVER = (Brush)App.Current.Resources["ItemHighlight"];
        public static readonly Brush SELECTED = (Brush)App.Current.Resources["PanelBackground"];
        public static readonly Brush HEADER = (Brush)App.Current.Resources["DarkComplement"];

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; toggleSelected(); }
        }
        private bool header = false;
        private bool removable = false;

        public AbstractListItem()
        {
            InitializeComponent();
            Background = WHITE;
        }

        public void First(String s)
        {
            date.Content = s;
        }

        public void Second(String s)
        {
            order.Content = s;
        }

        public void Third(String s)
        {
            total.Content = s;
        }

        public void MakeHeader() {
            Background = HEADER;
            header = true;
        }

        public void MakeRemovable()
        {
            removable = true;
        }

        private void toggleSelected()
        {
            if (header) return;
            Background = selected ? SELECTED : WHITE;
        }

        private void BGMouseEnter(object sender, MouseEventArgs e)
        {
            if (selected || header) return;
            Background = HOVER;
            if (removable)
            {
                removeButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void BGMouseLeave(object sender, MouseEventArgs e)
        {
            toggleSelected();
            if (removable)
            {
                removeButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
