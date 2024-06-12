using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiClient
{
    public partial class CitizenViewModel : ObservableObject
    {
        RestService restService = new RestService("http://localhost:54726/");

        [ObservableProperty]
        ObservableCollection<Citizen> citizens;

        [ObservableProperty]
        ObservableCollection<Settlement> settlements;

        [ObservableProperty]
        ObservableCollection<Country> countries;

        [ObservableProperty]
        Citizen selectedCitizen;

        [ObservableProperty]
        bool isBusy;

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

        private string _selectedSettlementName;
        public string SelectedSettlementName
        {
            get => _selectedSettlementName;
            set
            {
                if (_selectedSettlementName != value)
                {
                    _selectedSettlementName = value;
                    OnPropertyChanged();
                    UpdateSelectedCountry();
                }
            }
        }

        public ObservableCollection<string> CountryNames { get; private set; }

        public ObservableCollection<string> SettlementNames { get; private set; }


        public CitizenViewModel()
        {
            citizens = new ObservableCollection<Citizen>();
            settlements = new ObservableCollection<Settlement>();
            countries = new ObservableCollection<Country>();
            CountryNames = new ObservableCollection<string>();
            SettlementNames = new ObservableCollection<string>();
            GetAllDatasAsync();
        }

        async Task GetAllDatasAsync()
        {
            await GetCitizensAsync();
            await GetSettlementsAsync();
            await GetCountriesAsync();
        }

        async Task GetCitizensAsync()
        {
            IsBusy = true;
            citizens.Clear();
            var list = await restService.GetAsync<Citizen>("citizen");
            list.ForEach(citizen => citizens.Add(citizen));
            IsBusy = false;
        }

        async Task GetSettlementsAsync()
        {
            IsBusy = true;
            settlements.Clear();
            SettlementNames.Clear();
            var list = await restService.GetAsync<Settlement>("settlement");
            list.ForEach(settlement => settlements.Add(settlement));
            foreach(var settlement in settlements)
            {
                SettlementNames.Add(settlement.SettlementName);
            }
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
            if (SelectedCitizen != null && !string.IsNullOrEmpty(SelectedCountryName))
            {
                var selectedCountry = countries.FirstOrDefault(c => c.Name == SelectedCountryName);
                if (selectedCountry != null)
                {
                    SelectedCitizen.CitizenshipID = selectedCountry.CountryID;
                    SelectedCitizen.Citizenship = selectedCountry;
                }
            }
        }

        private void UpdateSelectedSettlement()
        {
            if (SelectedCitizen != null && !string.IsNullOrEmpty(SelectedSettlementName))
            {
                var selectedSettlement = settlements.FirstOrDefault(c => c.SettlementName == SelectedSettlementName);
                if (selectedSettlement != null)
                {
                    SelectedCitizen.SettlementID = selectedSettlement.SettlementID;
                    SelectedCitizen.Settlement = selectedSettlement;
                }
            }
        }

        [RelayCommand]
        async Task UpdateCitizenAsync()
        {
            await restService.PutAsync<Citizen>(SelectedCitizen, "citizen");
            await Shell.Current.DisplayAlert("Update", "Update successful.", "OK");
            await GetCitizensAsync();
        }

        [RelayCommand]
        async Task DeleteCitizenAsync()
        {
            await restService.DeleteAsync(SelectedCitizen.PersonID, "citizen");
            await Shell.Current.DisplayAlert("Delete", "Delete successful.", "OK");
            await GetCitizensAsync();
        }

        [RelayCommand]
        async Task CreateCitizenAsync()
        {
            await restService.PostAsync<Citizen>(new Citizen() { Name = new Guid().ToString(), BirthDate=new DateTime(2000,01,01), CitizenshipID=1, SettlementID=1, IncomeInUSD=10000,HasCriminalRecord=false }, "citizen");
            await Shell.Current.DisplayAlert("Create", "New citizen created with default values.", "OK");
            await GetCitizensAsync();
        }
    }
}
