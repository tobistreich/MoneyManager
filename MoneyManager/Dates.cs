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
    internal class Dates
    {
        private int fontsize = 15;
        private int height = 30;
        private DatePicker datesPicker;
        private StackPanel dateStackpanel;

        public Dates(StackPanel stackPanel, DatePicker datePicker)
        {
            dateStackpanel = stackPanel;
            datesPicker = datePicker;
        }

        public void AddDate(bool isIncome)
        {
            if (datesPicker != null && datesPicker.Text != null)
            {
                string date = datesPicker.Text;
                Label newLabel = new Label();
                newLabel.FontSize = fontsize;
                newLabel.Height = height;
                newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                newLabel.Content = date;
                dateStackpanel.Children.Add(newLabel);

                if (isIncome == true)
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
