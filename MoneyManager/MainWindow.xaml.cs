﻿using System;
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
        public DateTime Date {  get; set; }
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
            updateBalance();
            New_inc_exp();
        }
        private void Expense_Clicked(object sender, RoutedEventArgs e)
        {
            isIncome = false;
            updateBalance();
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
                getIncomePerDay();
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
                balanceLabel.Background= Brushes.LightGreen;
            }
            else if (balance < 0)
            {
                balanceLabel.Background= Brushes.IndianRed;
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

                records = records.OrderBy(r => r.Date).ToList();

                foreach (var record in records)
                {
                    DateTime dateWithoutTime = record.Date.Date;
                    NewLabel(record.Category, categoryStackpanel, record.IsIncome);
                    NewLabel(record.Name, nameStackpanel, record.IsIncome);
                    NewLabel(record.Amount.ToString(), amountStackpanel, record.IsIncome);
                    NewLabel(record.Date.ToString(), dateStackpanel, record.IsIncome);

                    if (record.IsIncome == true)
                    {
                        balance += Convert.ToDouble(record.Amount);
                    }
                    else if (!record.IsIncome)
                    {
                        balance -= Convert.ToDouble(record.Amount);
                    }
                    balance = Math.Round(balance, 2);
                    balanceLabel.Content = balance + "€";
                    getIncomePerDay();
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
        private void RestartWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        public void updateBalance()
        {
            if (isIncome)
            {
                balance += Convert.ToDouble(amountTextbox.Text);
                balance = Math.Round(balance, 2);
                balanceLabel.Content = balance + "€";
            }
            else
            {
                balance -= Convert.ToDouble(amountTextbox.Text);
                balance = Math.Round(balance, 2);
                balanceLabel.Content = balance + "€";
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
    }
}