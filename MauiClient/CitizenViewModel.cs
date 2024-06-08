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
        Citizen selectedCitizen;

        [ObservableProperty]
        bool isBusy;

        public CitizenViewModel()
        {
            citizens = new ObservableCollection<Citizen>();
            GetCitizensAsync();
        }

        async Task GetCitizensAsync()
        {
            IsBusy = true;
            citizens.Clear();
            var list = await restService.GetAsync<Citizen>("citizen");
            list.ForEach(citizen => citizens.Add(citizen));
            IsBusy = false;
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
