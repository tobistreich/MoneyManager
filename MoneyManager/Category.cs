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
        private ComboBox Category_ComboBox; 
        private StackPanel Category_StackPanel; 

        public Category(StackPanel stackPanel, ComboBox comboBox)
        {
            Category_StackPanel = stackPanel; 
            Category_ComboBox = comboBox; 
        }

        public void add_cat_label()
        {
            if (Category_ComboBox != null && Category_ComboBox.SelectedItem != null)
            {
                string category = Category_ComboBox.Text;
                Label new_category_label = new Label();
                new_category_label.FontSize = fontsize;
                new_category_label.Height = height;
                new_category_label.HorizontalContentAlignment = HorizontalAlignment.Center;
                new_category_label.Content = category;
                Category_StackPanel.Children.Add(new_category_label);
            }
        }
    }
}
