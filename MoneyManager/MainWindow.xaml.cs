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

        public MainWindow()
        {
            InitializeComponent();
            setNull();
        }
        public void add_inc_exp(object sender, RoutedEventArgs e)
        {
            Category_Class categoryClass = new Category_Class(Category_StackPanel, Category_ComboBox);
            Name_Class nameClass = new Name_Class(Name_StackPanel, Name_TextBox);
            Amount_Class amountClass = new Amount_Class(Amount_StackPanel, Amount_TextBox);
            Balance_Class balanceClass = new Balance_Class(Balance_Label, Amount_TextBox);

            if (Category_ComboBox == null || Name_TextBox == null || Amount_TextBox == null)
            {
                MessageBox.Show("Bitte überprüfe deine Eingaben nocheinmal!");        
            }
            else
            {
                categoryClass.add_cat_label();
                nameClass.add_name_label();
                amountClass.add_amount();

                setNull();
            }

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

        public void setNull()
        {
            Category_ComboBox.Text = null;
            Name_TextBox.Text = "";
            Amount_TextBox.Text = "";
        }
    }
}