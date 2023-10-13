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
        private MainWindow mainWindow;

        public Balance(MainWindow main, Label label, TextBox textBox)
        {
            mainWindow = main;
            Balanace_Label = label;
            Amount_TextBox = textBox;
        }
        public void UpdateBalance(double newBalance)
        {
            // Hier wird die balance-Variable im MainWindow aktualisiert
            mainWindow.balance = newBalance;
            mainWindow.UpdateBalanceLabel(); // Die Methode im MainWindow aufrufen, um das Label zu aktualisieren
        }

    }
}
