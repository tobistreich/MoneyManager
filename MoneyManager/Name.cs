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
        private TextBox nameTextbox;
        private StackPanel nameStackpanel;

        public Name(StackPanel stackPanel, TextBox textBox)
        {
            nameStackpanel = stackPanel;
            nameTextbox = textBox;
        }

        public void add_name_label()
        {
            if (nameTextbox != null && !string.IsNullOrWhiteSpace(nameTextbox.Text))
            {
                {
                    string name = nameTextbox.Text;
                    Label newLabel = new Label();
                    newLabel.FontSize = fontsize;
                    newLabel.Height = height;
                    newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newLabel.Content = name;
                    nameStackpanel.Children.Add(newLabel);
                }
            }
        }
    }
}
