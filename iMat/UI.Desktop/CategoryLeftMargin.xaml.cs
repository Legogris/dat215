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
            categoryStackPanel.Children.Clear();
            Label rootLabel = new Label() {
                Content = rootCategory.Name,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
            };
            categoryStackPanel.Children.Add(rootLabel);
            foreach (ProductCategory pc in rootCategory.SubCategories)
            {
                Label label = new Label();
                label.Content = pc.Name;
                categoryStackPanel.Children.Add(label);
            }
        }
    }
}
