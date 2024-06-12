using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiClient.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiClient
{
    public partial class SettlementViewModel : ObservableObject
    {
        private readonly RestService restService = new RestService("http://localhost:54726/");

        [ObservableProperty]
        private ObservableCollection<Settlement> settlements;

        [ObservableProperty]
        private ObservableCollection<Country> countries;

        [ObservableProperty]
        private bool isBusy;


        [ObservableProperty]
        private Settlement selectedSettlement;


        private string _selectedCountryName;
        public string SelectedCountryName
        {
            get => _selectedCountryName;
            set
            {
                if (_selectedCountryName != value)
                {
                    _selectedCountryName = value;
                    OnPropertyChanged();
                    UpdateSelectedCountry();
                }
            }
        }

        public ObservableCollection<string> CountryNames { get; private set; }

        public SettlementViewModel()
        {
            settlements = new ObservableCollection<Settlement>();
            countries = new ObservableCollection<Country>();
            CountryNames = new ObservableCollection<string>();
            GetAllDatasAsync();
        }

        private async Task GetAllDatasAsync()
        {
            await GetCountriesAsync();
            await GetSettlementsAsync();
        }

        private async Task GetSettlementsAsync()
        {
            IsBusy = true;
            settlements.Clear();
            var list = await restService.GetAsync<Settlement>("settlement");
            list.ForEach(settlement => settlements.Add(settlement));
            IsBusy = false;
        }

        private async Task GetCountriesAsync()
        {
            IsBusy = true;
            countries.Clear();
            CountryNames.Clear();
            var list = await restService.GetAsync<Country>("country");
            list.ForEach(country => countries.Add(country));
            foreach (var country in countries)
            {
                CountryNames.Add(country.Name);
            }
            IsBusy = false;
        }

        private void UpdateSelectedCountry()
        {
            if (SelectedSettlement != null && !string.IsNullOrEmpty(SelectedCountryName))
            {
                var selectedCountry = countries.FirstOrDefault(c => c.Name == SelectedCountryName);
                if (selectedCountry != null)
                {
                    SelectedSettlement.CountryID = selectedCountry.CountryID;
                    SelectedSettlement.Country = selectedCountry;
                }
            }
        }

        [RelayCommand]
        public async Task UpdateSettlementAsync()
        {
            await restService.PutAsync(SelectedSettlement, "settlement");
            await Shell.Current.DisplayAlert("Update", "Update successful.", "OK");
            await GetAllDatasAsync();
        }

        [RelayCommand]
        public async Task DeleteSettlementAsync()
        {
            await restService.DeleteAsync(SelectedSettlement.SettlementID, "settlement");
            await Shell.Current.DisplayAlert("Delete", "Delete successful.", "OK");
            await GetAllDatasAsync();
        }

        [RelayCommand]
        public async Task CreateSettlementAsync()
        {
            await restService.PostAsync(new Settlement() { SettlementName = Guid.NewGuid().ToString(), CountryID = 1, HDI = 0.9, Population = 100000 }, "settlement");
            await Shell.Current.DisplayAlert("Create", "New settlement created with default values.", "OK");
            await GetAllDatasAsync();
        }
    }

}
