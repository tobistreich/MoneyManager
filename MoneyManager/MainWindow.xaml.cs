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
        public double balance = 0.0;
        public MainWindow()
        {
            InitializeComponent();


            setNull();
  
        }
        public void add_inc_exp(object sender, RoutedEventArgs e)
        {
            Category categoryClass = new Category(Category_StackPanel, Category_ComboBox);
            Name nameClass = new Name(Name_StackPanel, Name_TextBox);
            Amount amountClass = new Amount(Amount_StackPanel, Amount_TextBox);
            Balance balanceClass = new Balance(mainWindow, Balance_Label, Amount_TextBox);

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


        }


        private void Income_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = true;
            balance += Convert.ToDouble(Amount_TextBox.Text);
            Balance_Label.Content = "+" + balance;

        }
        private void Expense_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            balance -= Convert.ToDouble(Amount_TextBox.Text);
            Balance_Label.Content = balance;
        }

        public void setNull()
        {
            Category_ComboBox.Text = null;
            Name_TextBox.Text = "";
            Amount_TextBox.Text = "";
        }

        public void UpdateBalanceLabel()
        {  
            Balance_Label.Content = balance.ToString();
        }
    }
}