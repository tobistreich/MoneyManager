using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager
{
    internal class Category
    {
        private int fontsize = 15;
        private int height = 30;
        private ComboBox categoryCombobox; 
        private StackPanel categoryStackpanel; 

        public Category(StackPanel stackPanel, ComboBox comboBox)
        {
            categoryStackpanel = stackPanel; 
            categoryCombobox = comboBox; 
        }

        public void addCategory()
        {
            if (categoryCombobox != null && categoryCombobox.SelectedItem != null)
            {
                string category = categoryCombobox.Text;
                Label newLabel = new Label();
                newLabel.FontSize = fontsize;
                newLabel.Height = height;
                newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                newLabel.Content = category;
                categoryStackpanel.Children.Add(newLabel);
            }
        }
    }
}
