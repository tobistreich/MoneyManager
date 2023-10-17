using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyManager
{
    /// <summary>
    /// Interaktionslogik für Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        MainWindow mainWindow = new MainWindow();
        public Confirmation()
        {
            InitializeComponent();
        }

        private void noButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void yesButtonClicked(object sender, RoutedEventArgs e)
        {
            mainWindow.ResetAll();
            this.Close();
        }
    }
}
