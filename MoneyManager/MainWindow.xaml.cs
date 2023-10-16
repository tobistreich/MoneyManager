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
            balance += Convert.ToDouble(amountTextbox.Text);
            balanceLabel.Content = balance + "€";
            new_inc_exp();
        }
        private void Expense_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            balance -= Convert.ToDouble(amountTextbox.Text);
            balanceLabel.Content = balance + "€";
            new_inc_exp();
        }
        public void new_inc_exp()
        {
            Category categoryClass = new Category(categoryStackpanel, categoryCombobox);
            Name nameClass = new Name(nameStackpanel, nameTextbox);
            Amount amountClass = new Amount(amountStackpanel, amountTextbox);

            if (categoryCombobox == null || nameTextbox == null || amountTextbox == null)
            {
                MessageBox.Show("Bitte überprüfe deine Eingaben nocheinmal!");
            }
            else
            {
                categoryClass.addCategory(isIncome);
                nameClass.addName(isIncome);
                amountClass.addAmount(isIncome);

                setNull();
                CheckBalance();
            }
        }
        public void setNull()
        {
            categoryCombobox.Text = null;
            nameTextbox.Text = "";
            amountTextbox.Text = "";
        }
        public void CheckBalance()
        {
            if (balance > 0)
            {
                balanceLabel.Background= Brushes.Green;
            }
            else if (balance < 0)
            {
                balanceLabel.Background= Brushes.Red;
            }
            else
            {
                balanceLabel.Background = Brushes.Gray;
            }
        }
    }
}