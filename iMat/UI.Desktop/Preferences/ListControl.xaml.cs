using Data;
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

namespace UI.Desktop.Content
{
    /// <summary>
    /// Interaction logic for ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        private ShoppingListHandler listHandler;
        
        public IList<FavoriteList> ShoppingListsCollection
        { get { return listHandler.GetItems(); } }

        public ListControl(DataHandler dh)
        {
            listHandler = dh.GetFavorites();
            InitializeComponent();       
        }        
    }
}