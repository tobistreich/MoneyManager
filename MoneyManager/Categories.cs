using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoneyManager
{
    internal class Categories
    {
        private int fontsize = 15;
        private int height = 30;
        private ComboBox categoryCombobox; 
        private StackPanel categoryStackpanel;

        public Categories(StackPanel stackPanel, ComboBox comboBox)
        {
            categoryStackpanel = stackPanel; 
            categoryCombobox = comboBox; 
        }

        public void AddCategory(bool isIncome)
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
                if (isIncome == true )
                {
                    newLabel.Background = Brushes.LightGreen;
                }
                else
                {
                    newLabel.Background = Brushes.OrangeRed;
                }
            }
        }
    }
}
