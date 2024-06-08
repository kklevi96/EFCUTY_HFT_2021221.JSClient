using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MauiClient
{
    public partial class CountryViewModel : ObservableObject
    {
        RestService restService = new RestService("http://localhost:54726/");

        [ObservableProperty]
        ObservableCollection<Country> countries;

        [ObservableProperty]
        Country selectedCountry;

        [ObservableProperty]
        bool isBusy;

        public CountryViewModel()
        {
            countries = new ObservableCollection<Country>();
            GetCountriesAsync();
        }

        async Task GetCountriesAsync()
        {
            IsBusy = true;
            countries.Clear();
            var list = await restService.GetAsync<Country>("country");
            list.ForEach(country => countries.Add(country));
            IsBusy = false;
        }

        [RelayCommand]
        async Task UpdateCountryAsync()
        {
            await restService.PutAsync<Country>(SelectedCountry, "country");
            await Shell.Current.DisplayAlert("Update", "Update successful.", "OK");
            await GetCountriesAsync();
        }

        [RelayCommand]
        async Task DeleteCountryAsync()
        {
            await restService.DeleteAsync(SelectedCountry.CountryID, "country");
            await Shell.Current.DisplayAlert("Delete", "Delete successful.", "OK");
            await GetCountriesAsync();
        }

        [RelayCommand]
        async Task CreateCountryAsync()
        {
            await restService.PostAsync<Country>(new Country() { Name = new Guid().ToString(), IsOECDMember=false,TotalGDPInMillionUSD=10000 }, "country");
            await Shell.Current.DisplayAlert("Create", "New country created with default values.", "OK");
            await GetCountriesAsync();
        }
    }
}
