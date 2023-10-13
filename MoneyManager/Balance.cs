using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager
{
    internal class Balance
    {
        private TextBox Amount_TextBox;
        private Label Balanace_Label;

        public Balance(Label label, TextBox textBox)
        {
            Balanace_Label = label;
            Amount_TextBox = textBox;
        }
        public void updateBalance()
        {
            double balance = Convert.ToDouble(Balanace_Label.Content);
            double amount = Convert.ToDouble(Amount_TextBox);
            balance += amount;
            Balanace_Label.Content = balance;
        }
    }
}
