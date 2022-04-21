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

namespace EFCUTY_HFT_2021221.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCty_Click(object sender, RoutedEventArgs e)
        {
            CountryWindow countryWindow=new CountryWindow();
            countryWindow.Show();
        }

        private void ButtonStl_Click(object sender, RoutedEventArgs e)
        {
            SettlementWindow settlementWindow = new SettlementWindow();
            settlementWindow.Show();
        }

        private void ButtonCtn_Click(object sender, RoutedEventArgs e)
        {
            CitizenWindow citizenWindow = new CitizenWindow();
            citizenWindow.Show();
        }
    }
}
