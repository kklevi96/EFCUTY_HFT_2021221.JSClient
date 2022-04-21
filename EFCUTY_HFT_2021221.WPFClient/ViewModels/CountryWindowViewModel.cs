using EFCUTY_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EFCUTY_HFT_2021221.WPFClient
{
    public class CountryWindowViewModel : ObservableRecipient
    {
        public RestCollection<Country> Countries { get; set; }
        private Country selectedCountry;

        public Country SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                if (value != null)
                {
                    selectedCountry = new Country()
                    {
                        Name = value.Name,
                        CountryID = value.CountryID,
                        IsOECDMember = value.IsOECDMember,
                        TotalGDPInMillionUSD = value.TotalGDPInMillionUSD,
                        //Settlements = value.Settlements,
                        //Citizens = value.Citizens
                    };
                    OnPropertyChanged();
                }
                ((RelayCommand)CreateCountryCommand).NotifyCanExecuteChanged();
                ((RelayCommand)DeleteCountryCommand).NotifyCanExecuteChanged();
                ((RelayCommand)UpdateCountryCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateCountryCommand { get; set; }
        public ICommand DeleteCountryCommand { get; set; }
        public ICommand UpdateCountryCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CountryWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Countries = new RestCollection<Country>("http://localhost:54726/", "country", "hub");
                CreateCountryCommand = new RelayCommand(() =>
                {                        
                    Countries.Add(new Country()
                    {
                        Name = SelectedCountry.Name,
                        TotalGDPInMillionUSD = SelectedCountry.TotalGDPInMillionUSD,
                        IsOECDMember = SelectedCountry.IsOECDMember
                    });
                });

                UpdateCountryCommand = new RelayCommand(() =>
                {
                    Countries.Update(SelectedCountry);
                },
                () =>
                {
                    return SelectedCountry != null;
                });

                DeleteCountryCommand = new RelayCommand(() =>
                {
                    Countries.Delete(SelectedCountry.CountryID);
                },
                () =>
                {
                    return SelectedCountry != null;
                });

                //ha kijelölés nélkül hozunk létre, ne fusson hibára - így viszont nem működik, hogy ne legyen inaktív a delete/update gomb, ha semmi sincs kiválasztva, hiszen minimum egy default
                //ki lesz választva
                SelectedCountry = new Country();
            }
        }
    }
}
