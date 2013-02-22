﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner : UserControl
    {
        private double step = 1;
        private double value;
        private bool enabled = false;
        public delegate void ValueChangedHandler(Spinner sender);
        public event ValueChangedHandler ValueChanged;

        public double Value {
            get { return value; }
            set { this.value = value; }
        }
        public double Step {
            get { return step; }
            set { step = value; this.value = value; UpdateTextField(); }
        }
        public bool Enabled {
            get { return enabled; }
            set { enabled = value; toggleState(); }
        }

        public Spinner()
        {
            InitializeComponent();
            
            toggleState();
        }
        
        private void spinnerIncrease(object sender, RoutedEventArgs e)
        {
            value = value+step;
            UpdateTextField();
        }

        private void spinnerDecrease(object sender, RoutedEventArgs e)
        {
            value = value-step;
            if (value < step)
            {
                value = step;
            }
            UpdateTextField();
        }

        private void toggleState()
        {
            SpinnerTextBox.IsEnabled = enabled;
            SpinnerIncrease.IsEnabled = enabled;
            SpinnerDecrease.IsEnabled = enabled;
            checkDecreasePossible();
        }

        public void UpdateTextField()
        {
            SpinnerTextBox.Text = value.ToString();
        }

        private void SpinnerTextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = isTextAllowed(e.Text);
        }

        private bool isTextAllowed(String s)
        {
            Regex regex = new Regex("[^0-9,]+"); // tillåter oändligt många decimaltecken
            return regex.IsMatch(s);
        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            if (SpinnerTextBox.Text.Length != 0)
            {
                value = Convert.ToDouble(SpinnerTextBox.Text);
            }
            else {
                value = step;
            }
            checkDecreasePossible();

            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this);
            }
        }

        private void checkDecreasePossible() {
            SpinnerDecrease.IsEnabled = (value != step) && enabled;
        }
    }
}
