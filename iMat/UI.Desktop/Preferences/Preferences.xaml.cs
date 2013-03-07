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
        private AccountControl account;
        private ListControl shoppinglist;

        public enum StartupView
        {
            Account, List
        }

        public PreferencesWindow(DataHandler dh, StartupView open)
        {
            InitializeComponent();
            dataHandler = dh;
            account = new AccountControl(dataHandler);
            account.SettingsChanged += account_SettingsChanged;
            shoppinglist = new ListControl(dataHandler);
            addNav(account, "Konto", (BitmapImage)App.Current.Resources["UserImage"]);
            addNav(shoppinglist, "Shoppinglistor", (BitmapImage)App.Current.Resources["ShoppingListImage"]);
            
            if (open == StartupView.Account)
            {
                content.Children.Add(account);
                prefTabs[0].Selected = true;
            }
            else
            {
                content.Children.Add(shoppinglist);
                prefTabs[1].Selected = true;
            }
        }

        void account_SettingsChanged(UserControl sender)
        {
            okButton.Content = "Spara";
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
            foreach (PreferenceTab tabx in prefTabs) {
                tabx.Selected = false;
            }
            PreferenceTab tab = sender as PreferenceTab;
            if (tab!= null)
            {
                content.Children.Add(tab.DataContext as UserControl);
                tab.Selected = true;
            }
        }

        private void okClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            account.save();
        }

        private void openPane(StartupView view)
        {
            content.Children.Clear();
            foreach (PreferenceTab tabx in prefTabs)
            {
                tabx.Selected = false;
            }
            if (view == StartupView.Account)
            {
                content.Children.Add(account);
                prefTabs[0].Selected = true;
            }
            else
            {
                content.Children.Add(shoppinglist);
                prefTabs[1].Selected = true;
            }
        }

        private void commandBinding_Preferences(object sender, ExecutedRoutedEventArgs e)
        {
            if (prefTabs[1].Selected) return;
            openPane(StartupView.List);
        }

        private void commandBinding_ShoppingList(object sender, ExecutedRoutedEventArgs e)
        {
            if (prefTabs[0].Selected) return;
            openPane(StartupView.Account);
        }
    }
}
