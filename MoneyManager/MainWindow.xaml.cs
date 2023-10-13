using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace MoneyManager
{
    public partial class MainWindow : Window
    {
        
        public bool isIncome;
        int fontsize = 15;
        int height = 30;
        double balance = 0;

        public MainWindow()
        {
            InitializeComponent();
            CSV_Init_isIncome();
            CSV_Init_Category();
            CSV_Init_Name();
            CSV_Init_Amount();
            CheckBalance();     
        }
        private void add_inc_exp(object sender, RoutedEventArgs e)
        {
            string name = Name_TextBox.Text;
            double amount = Convert.ToDouble(Amount_TextBox.Text);
            #region new category

            CSV_Add_Category();

            #region add category label
            string category = Category_ComboBox.Text;
            Label new_category_label = new Label();
            new_category_label.FontSize = fontsize;
            new_category_label.Height = height;
            new_category_label.HorizontalContentAlignment = HorizontalAlignment.Center;
            new_category_label.Content = Category_ComboBox.Text;
            Category_StackPanel.Children.Add(new_category_label);
            #endregion

            #endregion

            #region new name
            CSV_Add_Name();

            #region add name label
            Label new_name_label = new Label();
            new_name_label.FontSize = fontsize;
            new_name_label.Height = height;
            new_name_label.Content = Name_TextBox.Text;
            new_name_label.HorizontalContentAlignment = HorizontalAlignment.Center;
            Name_StackPanel.Children.Add(new_name_label);
            #endregion

            if (isIncome == true)
            {
                new_name_label.Background = Brushes.Green;
            }
            else
            {
                new_name_label.Background = Brushes.Red;
            }

            Name_TextBox.Text = "";
            
            #endregion

            #region new amount
            CSV_Add_Amount();

            Label new_amount_label = new Label();
            new_amount_label.FontSize = fontsize;
            new_amount_label.Height = height;
            new_amount_label.HorizontalContentAlignment = HorizontalAlignment.Center;
            if (isIncome == true)
            {
                new_amount_label.Background = Brushes.Green;
                new_amount_label.Content = "+" + Amount_TextBox.Text + "€";
               
            }
            else
            {
                new_amount_label.Background = Brushes.Red;
                new_amount_label.Content = "-" + Amount_TextBox.Text + "€";
                
            }
            if (isIncome == false)
            {
                balance -= amount;
            }
            else if (isIncome == true)
            {
                balance -= amount;
            }

            Amount_TextBox.Text = "";
            Amount_StackPanel.Children.Add(new_amount_label);
            #endregion

            

            CSV_Add_IsIncome();
            CheckIncome();
            CheckBalance();
            Add_Button.Visibility = Visibility.Collapsed; 
        }


        private void Income_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = true;
            Add_Button.Visibility = Visibility.Visible;
        }
        private void Expense_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            Add_Button.Visibility = Visibility.Visible;
        }

        private void CheckBalance()
        {
            
            if (balance == 0) 
            {
                Balance_Label.Background = Brushes.Gray;
            }
            else if (balance > 0) 
            {
                Balance_Label.Background = Brushes.Green;
            }
            else if (balance < 0)
            {
                Balance_Label.Background = Brushes.Red;
            }
            Balance_Label.Content = balance.ToString() + "€";
        }
        private void CheckIncome()
        {
            
        }

        private void CSV_Add_Category()
        {
            string category = Category_ComboBox.Text;
            using (var writer = new StreamWriter("category.csv", true))
            {
                writer.WriteLine(category);
            }    
        }
        private void CSV_Add_Name()
        {
            string name = Name_TextBox.Text;
            using (var writer = new StreamWriter("name.csv", true))
            {
                writer.WriteLine(name);
            }
        }
        private void CSV_Add_Amount()
        {
            string amount = Convert.ToString(Amount_TextBox.Text);
            using (var writer = new StreamWriter("amount.csv", true))
            {
                writer.WriteLine(amount);
            }
        }
        private void CSV_Add_IsIncome()
        {
            Convert.ToString(isIncome);
            using (var writer = new StreamWriter("isIncome.csv", true))
            {
                writer.WriteLine(isIncome);
            }
        }

        private void CSV_Init_Category()
        {
            string[] csvCategorys = File.ReadAllLines("category.csv");
            foreach (string csvCategory in csvCategorys)
            {
                Label new_init_label = new Label();
                new_init_label.Content = csvCategory;
                new_init_label.FontSize = fontsize;
                new_init_label.Height = height;
                new_init_label.HorizontalContentAlignment = HorizontalAlignment.Center;

                if (isIncome == true)
                {
                    new_init_label.Background = Brushes.Green;
                }
                else
                {
                    new_init_label.Background = Brushes.Red;
                }
                Category_StackPanel.Children.Add(new_init_label);
            }
        }
        private void CSV_Init_Name()
        {
            string[] csvNames = File.ReadAllLines("name.csv");
            foreach (string csvName in csvNames)
            {
                Label new_init_label = new Label();
                new_init_label.Content = csvName;
                new_init_label.FontSize = fontsize;
                new_init_label.Height = height;
                new_init_label.HorizontalContentAlignment = HorizontalAlignment.Center;
                
                if (isIncome == true)
                {
                    new_init_label.Background = Brushes.Green;
                }
                else
                {
                    new_init_label.Background = Brushes.Red;
                }
                Name_StackPanel.Children.Add(new_init_label);
            }
        }
        private void CSV_Init_Amount()
        {
            string[] csvAmounts = File.ReadAllLines("amount.csv");
            foreach (string csvAmount in csvAmounts)
            {
                Label new_init_label = new Label();
                new_init_label.FontSize = fontsize;
                new_init_label.Height = height;
                new_init_label.HorizontalContentAlignment = HorizontalAlignment.Center;
                if (isIncome == true)
                {
                    new_init_label.Background = Brushes.Green;
                    new_init_label.Content = "+" + csvAmount + "€";
                }
                else if (isIncome == false)
                {
                    new_init_label.Background = Brushes.Red;
                    new_init_label.Content = "-" + csvAmount + "€";
                }
                Amount_StackPanel.Children.Add(new_init_label);
            }
        }
        private void CSV_Init_isIncome()
        {
            string[] csvisIncomes = File.ReadAllLines("isIncome.csv");
            foreach (string csvisIncome in csvisIncomes)
            {
                Label new_init_label = new Label();
                new_init_label.Content = csvisIncome;
                new_init_label.FontSize = fontsize;
                new_init_label.Height = height;
                new_init_label.HorizontalContentAlignment = HorizontalAlignment.Center;
                isIncome = Convert.ToBoolean(csvisIncome);
            }
        }
    }
}