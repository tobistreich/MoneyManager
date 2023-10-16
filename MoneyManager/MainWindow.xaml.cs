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
        public string Amount { get; set; }
        public bool IsIncome { get; set; }
        public string Date {  get; set; }
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
            CheckBalanceColor();
            SetNull();
        }
        


        private void Income_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = true;
            balance += Convert.ToDouble(amountTextbox.Text);
            balanceLabel.Content = balance + "€";
            New_inc_exp();
        }
        private void Expense_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            balance -= Convert.ToDouble(amountTextbox.Text);
            balanceLabel.Content = balance + "€";
            New_inc_exp();
        }
        public void New_inc_exp()
        {
            Categories categoryClass = new Categories(categoryStackpanel, categoryCombobox);
            Names nameClass = new Names(nameStackpanel, nameTextbox);
            Amounts amountClass = new Amounts(amountStackpanel, amountTextbox);
            Dates datesClass = new Dates(dateStackpanel, datesPicker);

            if (categoryCombobox == null || nameTextbox == null || amountTextbox == null)
            {
                MessageBox.Show("Bitte überprüfe deine Eingaben nocheinmal!");
            }
            else
            {
                categoryClass.addCategory(isIncome);
                nameClass.addName(isIncome);
                amountClass.addAmount(isIncome);
                datesClass.addDate(isIncome);

                WriteToCSV();
                SetNull();
                CheckBalanceColor();
            }
        }
        public void SetNull()
        {
            categoryCombobox.Text = null;
            nameTextbox.Text = "";
            amountTextbox.Text = "";
            datesPicker.Text = null;
        }
        public void CheckBalanceColor()
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
                Amount = amountTextbox.Text,
                IsIncome = isIncome,
                Date = datesPicker.Text,
            };

            records.Add(record); 

            using (var writer = new StreamWriter("data.csv", false)) // Truncate the file
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }
        public void ReadCsvData()
        {
            using (var reader = new StreamReader("data.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<ExpenseRecord>().ToList();

                categoryStackpanel.Children.Clear();
                nameStackpanel.Children.Clear();
                amountStackpanel.Children.Clear();
                dateStackpanel.Children.Clear();

                foreach (var record in records)
                {
                    NewLabel(record.Category, categoryStackpanel, record.IsIncome);
                    NewLabel(record.Name, nameStackpanel, record.IsIncome);
                    NewLabel(record.Amount.ToString(), amountStackpanel, record.IsIncome);
                    NewLabel(record.Date, dateStackpanel, record.IsIncome);

                    if (record.IsIncome == true)
                    {
                        balance += Convert.ToDouble(record.Amount);
                    }
                    else if (!record.IsIncome)
                    {
                        balance -= Convert.ToDouble(record.Amount);
                    }
                    balanceLabel.Content = balance + "€";
                }
            }
        }
        public void NewLabel(string content, StackPanel stackpanel, bool isIncome)
        {
            Label newLabel = new Label();
            newLabel.Content = content;
            newLabel.FontSize = 15;
            newLabel.Height = 30;
            newLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            if (isIncome)
            {
                newLabel.Background = Brushes.Green;
            }
            else
            {
                newLabel.Background = Brushes.Red;
            }
            if (stackpanel == amountStackpanel && isIncome == true)
            {
                newLabel.Content = content + "€";
            }
            else if (stackpanel == amountStackpanel && isIncome == false)
            {
                newLabel.Content = "-" + content + "€";
            }

            stackpanel.Children.Add(newLabel);
        }
    }
}