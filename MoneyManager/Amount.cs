using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager
{
    internal class Amount
    {
        private int fontsize = 15;
        private int height = 30;
        private TextBox Amount_TextBox;
        private StackPanel Amount_StackPanel;
        

        public Amount(StackPanel stackPanel, TextBox textBox)
        {
            Amount_StackPanel = stackPanel;
            Amount_TextBox = textBox;

        }

        public void add_amount()
        {
            if (Amount_TextBox != null && !string.IsNullOrWhiteSpace(Amount_TextBox.Text))
            {
                {
                    string amount = Amount_TextBox.Text;
                    Label new_label = new Label();
                    new_label.FontSize = fontsize;
                    new_label.Height = height;
                    new_label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    new_label.Content = amount;
                    Amount_StackPanel.Children.Add(new_label);
                }
            }
        }
    }
}
