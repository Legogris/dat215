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
using UI.Desktop.Preferences.Content;

namespace UI.Desktop.Preferences
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        private DataHandler dataHandler;
        private List<PreferenceTab> prefTabs = new List<PreferenceTab>();

        public PreferencesWindow(DataHandler dh, UserControl open)
        {
            InitializeComponent();
            dataHandler = dh;
            content.Children.Add(open);

            addNav(new AccountControl(dataHandler), "Konto", (BitmapImage)App.Current.Resources["UserImage"]);
            addNav(new ListControl(dataHandler), "Shoppinglistor", (BitmapImage)App.Current.Resources["ShoppingListImage"]);

            if (open is AccountControl)
            {
                prefTabs[0].Selected = true;
            }
            else
            {
                prefTabs[1].Selected = true;
            }
        }

        private void addNav(UserControl control, String name, BitmapImage image)
        {
            PreferenceTab tab = new PreferenceTab(name, image);
            tab.DataContext = control;
            navigation.Children.Add(tab);
            tab.MouseUp += tab_MouseUp;
            prefTabs.Add(tab);
        }

        void tab_MouseUp(object sender, MouseButtonEventArgs e)
        {
            content.Children.Clear();
            PreferenceTab tab = sender as PreferenceTab;
            if (tab!= null)
            {
                content.Children.Add(tab.DataContext as UserControl);
            }
        }

        private void okClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
