using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    }
}
