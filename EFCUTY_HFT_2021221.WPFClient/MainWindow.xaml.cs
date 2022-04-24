using System.Windows;

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
            CountryWindow countryWindow = new CountryWindow();
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
