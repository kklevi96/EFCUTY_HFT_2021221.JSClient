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
    public partial class SettlementViewModel : ObservableObject
    {
        RestService restService = new RestService("http://localhost:54726/");

        [ObservableProperty]
        ObservableCollection<Settlement> settlements;

        [ObservableProperty]
        Settlement selectedSettlement;

        [ObservableProperty]
        bool isBusy;

        public SettlementViewModel()
        {
            settlements = new ObservableCollection<Settlement>();
            GetSettlementsAsync();
        }

        async Task GetSettlementsAsync()
        {
            IsBusy = true;
            settlements.Clear();
            var list = await restService.GetAsync<Settlement>("settlement");
            list.ForEach(settlement => settlements.Add(settlement));
            IsBusy = false;
        }

        [RelayCommand]
        async Task UpdateCountryAsync()
        {
            await restService.PutAsync<Settlement>(SelectedSettlement, "settlement");
            await Shell.Current.DisplayAlert("Update", "Update successful.", "OK");
            await GetSettlementsAsync();
        }

        [RelayCommand]
        async Task DeleteCountryAsync()
        {
            await restService.DeleteAsync(SelectedSettlement.SettlementID, "settlement");
            await Shell.Current.DisplayAlert("Delete", "Delete successful.", "OK");
            await GetSettlementsAsync();
        }

        [RelayCommand]
        async Task CreateSettlementAsync()
        {
            await restService.PostAsync<Settlement>(new Settlement() { SettlementName = new Guid().ToString() ,CountryID=1,HDI=0.9,Population=100000 }, "settlement");
            await Shell.Current.DisplayAlert("Create", "New settlement created with default values.", "OK");
            await GetSettlementsAsync();
        }
    }
}
