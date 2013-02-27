﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.Desktop
{
    public abstract class AbstractSelector : UserControl
    {
        public static readonly string CURRENCY = " kr";
        protected Product product;

        protected abstract Label PriceLabel { get; }
        protected abstract Label PieceLabel { get; }
        protected abstract Label JmfLabel { get; }
        protected abstract Spinner AmountSpinner { get; }
        public double NumberOfItems
        {
            get;
            protected set;
        }

        public AbstractSelector(Product p) {
            product = p;
        }

        protected void init() {
            updatePrice(1);
            updateJmf();
            AmountSpinner.Step = 1;
            NumberOfItems = AmountSpinner.Step;
            AmountSpinner.ValueChanged += spinner_AmountChanged;
        }

        protected void updatePrice(double multiplier)
        {
            PriceLabel.Content = (multiplier*product.Price) + CURRENCY;
        }

        private void updateJmf() {
            JmfLabel.Content = product.Price + " " + product.Unit;
            PieceLabel.Content = product.Price + CURRENCY + "/st";
        }

        protected void spinner_AmountChanged(Spinner spinner) {
            updatePrice(spinner.Value);
            NumberOfItems = spinner.Value;
        }
    }
}
