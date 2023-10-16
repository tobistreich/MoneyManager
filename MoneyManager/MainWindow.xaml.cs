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
using System.Linq;

namespace MoneyManager
{
    public class ExpenseRecord
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public bool IsIncome { get; set; }
    }
    public partial class MainWindow : Window
    {
        public bool isIncome;
        public double balance = 0.0;
        private List<ExpenseRecord> records = new List<ExpenseRecord>();

        public MainWindow()
        {
            InitializeComponent();

            ReadCsvData();
            CheckBalance();
            SetNull();
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
        public void New_inc_exp()
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

                WriteToCSV();
                SetNull();
                CheckBalance();
            }
        }
        public void SetNull()
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
        public void WriteToCSV()
        {
            var record = new ExpenseRecord
            {
                Category = categoryCombobox.Text,
                Name = nameTextbox.Text,
                Amount = Convert.ToDouble(amountTextbox.Text),
                IsIncome = isIncome
            };

            var records = new List<ExpenseRecord> { record };

            using (var writer = new StreamWriter("expenses.csv", true))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }
        public void ReadCsvData()
        {
            using (var reader = new StreamReader("expenses.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<ExpenseRecord>().ToList();

                if (records.Count > 0)
                {
                    var lastRecord = records.Last();
                    labelCategory.Content = lastRecord.Category;
                    labelName.Content = lastRecord.Name;
                    labelAmount.Content = lastRecord.Amount.ToString("C");
                }
            }
        }
    }
}