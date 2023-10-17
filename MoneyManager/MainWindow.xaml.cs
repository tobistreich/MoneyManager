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
    public class Entries
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public bool IsIncome { get; set; }
        public DateTime Date {  get; set; }
    }
    public partial class MainWindow : Window
    {
        public bool isIncome;
        public double balance = 0.0;
        private List<Entries> records = new List<Entries>();

        public MainWindow()
        {
            InitializeComponent();

            ReadCSV();
            SetNull();
        }

        private void IncomeButtonClicked(object sender, RoutedEventArgs e)
        {
            isIncome = true;
            updateBalance(Convert.ToDouble(amountTextbox.Text), isIncome);
            New_inc_exp();
        }
        private void ExpenseButtonClicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            updateBalance(Convert.ToDouble(amountTextbox.Text), isIncome);
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
                categoryClass.AddCategory(isIncome);
                nameClass.AddName(isIncome);
                amountClass.AddAmount(isIncome);
                datesClass.AddDate(isIncome);

                WriteCSV();
                SetNull();
                CheckBalanceColor();
                getIncomePerDay();
            }
        }
        public void WriteCSV()
        {
            var record = new Entries
            {
                Category = categoryCombobox.Text,
                Name = nameTextbox.Text,
                Amount = amountTextbox.Text,
                IsIncome = isIncome,
                Date = datesPicker.SelectedDate ?? DateTime.Now
            };

            records.Add(record); 

            using (var writer = new StreamWriter("data.csv", false)) // Truncate the file
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }

            records = records.OrderBy(r => r.Date).ToList();
        }
        public void ReadCSV()
        {
            using (var reader = new StreamReader("data.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<Entries>().ToList();

                categoryStackpanel.Children.Clear();
                nameStackpanel.Children.Clear();
                amountStackpanel.Children.Clear();
                dateStackpanel.Children.Clear();

                records = records.OrderBy(r => r.Date).ToList();

                foreach (var record in records)
                {
                    DateTime dateWithoutTime = record.Date.Date;

                    NewLabel(record.Category, categoryStackpanel, record.IsIncome);
                    NewLabel(record.Name, nameStackpanel, record.IsIncome);
                    NewLabel(record.Amount.ToString(), amountStackpanel, record.IsIncome);
                    NewLabel(record.Date.ToString(), dateStackpanel, record.IsIncome);

                    updateBalance(Convert.ToDouble(record.Amount), record.IsIncome);
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
                newLabel.Background = Brushes.LightGreen;
            }
            else
            {
                newLabel.Background = Brushes.OrangeRed;
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
        public void updateBalance(double amount, bool isIncome)
        {
            if (isIncome == true)
            {
                balance += Convert.ToDouble(amount);
                balance = Math.Round(balance, 2);
                balanceLabel.Content = balance + "€";
                CheckBalanceColor();
                getIncomePerDay();
            }
            else if (isIncome == false)
            {
                balance -= Convert.ToDouble(amount);
                balance = Math.Round(balance, 2);
                balanceLabel.Content = balance + "€";
                CheckBalanceColor();
                getIncomePerDay();
            }
        }
        public void CheckBalanceColor()
        {
            if (balance > 0)
            {
                balanceLabel.Background = Brushes.LightGreen;
            }
            else if (balance < 0)
            {
                balanceLabel.Background = Brushes.IndianRed;
            }
            else
            {
                balanceLabel.Background = Brushes.Gray;
            }
        }
        public void getIncomePerDay()
        {
            DateTime currentDate = DateTime.Now;
            DateTime lastDay = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));

            int daysLeft = (lastDay - currentDate).Days;
            double incomePerDay = balance / daysLeft;
            incomePerDay = Math.Round(incomePerDay, 2);

            if (incomePerDay > 0)
            {
                incomePerDayLabel.Background = Brushes.LightGreen;
                incomePerDayLabel.Content = incomePerDay.ToString() + "€";
            }
            else if (incomePerDay < 0)
            {
                incomePerDayLabel.Background = Brushes.OrangeRed;
            }
        }
        public void ResetAll()
        {
            categoryStackpanel.Children.Clear();
            nameStackpanel.Children.Clear();
            amountStackpanel.Children.Clear();
            dateStackpanel.Children.Clear();
            balance = 0.00;

            string filePath = "data.csv";
            File.WriteAllText(filePath, string.Empty);
            RestartWindow();
        }
        public void ResetButtonClicked(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation();
            confirmation.Show();
        }
        public void RestartWindow()
        {
            MainWindow newMainWindow = new MainWindow();
            newMainWindow.Show();
            this.Close();
        }
        private void RestartButtonClicked(object sender, RoutedEventArgs e)
        {
            RestartWindow();
        }
        public void SetNull()
        {
            categoryCombobox.Text = null;
            nameTextbox.Text = "";
            amountTextbox.Text = "";
            datesPicker.Text = null;
        }

    }
}