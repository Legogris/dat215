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
using Data;

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CategoryLeftMargin : UserControl
    {
        private ProductCategory rootCategory;

        public ProductCategory RootCategory
        {
            get { return rootCategory; }
            set { rootCategory = value;
                categorySourceUpdated(); }
        }
        public CategoryLeftMargin()
        {
            InitializeComponent();
        }
        
        private void categorySourceUpdated()
        {
            container.Children.Clear();
            CategoryControl root = new CategoryControl(rootCategory);
            container.Children.Add(root);
            root.Expand();
            Thickness margin = root.stackPanel.Margin;
            margin.Left = 0;
            root.stackPanel.Margin = margin;
        }
    }
}
