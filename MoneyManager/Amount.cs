﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoneyManager
{
    internal class Amount
    {
        private int fontsize = 15;
        private int height = 30;
        private TextBox amountTextbox;
        private StackPanel amountStackpanel;


        public Amount(StackPanel stackPanel, TextBox textBox)
        {
            amountStackpanel = stackPanel;
            amountTextbox = textBox;

        }

        public void addAmount(bool isIncome)
        {
            if (amountTextbox != null && !string.IsNullOrWhiteSpace(amountTextbox.Text))
            {
                string amount = amountTextbox.Text;
                Label newLabel = new Label();
                newLabel.FontSize = fontsize;
                newLabel.Height = height;
                newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                
                amountStackpanel.Children.Add(newLabel);
                if (isIncome == true)
                {
                    newLabel.Content = amount + "€";
                    newLabel.Background = Brushes.Green;
                }
                else
                {
                    newLabel.Content = "-" + amount + "€";
                    newLabel.Background = Brushes.Red;
                }
            }
        }
    }
}
