using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager
{
    internal class Name
    {
        private int fontsize = 15;
        private int height = 30;
        private TextBox Name_TextBox;
        private StackPanel Name_StackPanel;

        public Name(StackPanel stackPanel, TextBox textBox)
        {
            Name_StackPanel = stackPanel;
            Name_TextBox = textBox;
        }

        public void add_name_label()
        {
            if (Name_TextBox != null && !string.IsNullOrWhiteSpace(Name_TextBox.Text))
            {
                {
                    string name = Name_TextBox.Text;
                    Label new_label = new Label();
                    new_label.FontSize = fontsize;
                    new_label.Height = height;
                    new_label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    new_label.Content = name;
                    Name_StackPanel.Children.Add(new_label);
                }
            }
        }
    }
}
