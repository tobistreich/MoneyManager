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

            CheckBalance();
            setNull();
        }
        


        private void Income_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = true;
            balance += Convert.ToDouble(Amount_TextBox.Text);
            Balance_Label.Content = balance + "€";
            new_inc_exp();
        }
        private void Expense_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            balance -= Convert.ToDouble(Amount_TextBox.Text);
            Balance_Label.Content = balance + "€";
            new_inc_exp();
        }
        public void new_inc_exp()
        {
            Category categoryClass = new Category(Category_StackPanel, Category_ComboBox);
            Name nameClass = new Name(Name_StackPanel, Name_TextBox);
            Amount amountClass = new Amount(Amount_StackPanel, Amount_TextBox);

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
                CheckBalance();
            }
        }
        public void setNull()
        {
            Category_ComboBox.Text = null;
            Name_TextBox.Text = "";
            Amount_TextBox.Text = "";
        }
        public void CheckBalance()
        {
            if (balance > 0)
            {
                Balance_Label.Background= Brushes.Green;
            }
            else if (balance < 0)
            {
                Balance_Label.Background= Brushes.Red;
            }
            else
            {
                Balance_Label.Background = Brushes.Gray;
            }
        }
    }
}