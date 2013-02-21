﻿using Data;
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

namespace UI.Desktop
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        private ProductCategory category;
        
        public ProductCategory ProductCategory
        {
            get { return category; }
            set
            {
                category = value;
                categorySourceUpdated();
            }
        }

        public ListView()
        {
            InitializeComponent();
        }

        private void categorySourceUpdated()
        {
            foreach (Product product in category.GetProducts()) {
                stackPanel.Children.Add(new ListViewItem(product));
            }
        }
    }
}
