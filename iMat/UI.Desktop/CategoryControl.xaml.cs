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
    /// Interaction logic for CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        private ProductCategory productCategory;
        private bool expanded = false;
        public CategoryControl(ProductCategory pc)
        {
            InitializeComponent();
            productCategory = pc;
            categoryLabel.Content = pc.Name;
            categoryLabel.MouseUp += categoryLabel_MouseUp;
        }

        void categoryLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (expanded) {
                Retract();
            }
            else {
                Expand();
            }
        }

        public void Expand()
        {
            expanded = true;
            foreach (ProductCategory pc in productCategory.SubCategories)
            {
                CategoryControl c = new CategoryControl(pc);
                stackPanel.Children.Add(c);
            }
        }

        public void Retract() {
            expanded = false;
            stackPanel.Children.Clear();
        }
    }
}