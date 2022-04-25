using EFCUTY_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EFCUTY_HFT_2021221.WPFClient.ViewModels
{
    public class CitizenWindowViewModel : ObservableRecipient
    {
        public RestCollection<Citizen> Citizens { get; set; }
        public RestCollection<Settlement> Settlements { get; set; }
        public RestCollection<Country> Countries { get; set; }


        private Citizen selectedCitizen;

        public Citizen SelectedCitizen
        {
            get { return selectedCitizen; }
            set
            {
                if (value != null)
                {
                    selectedCitizen = new Citizen()
                    {
                        PersonID = value.PersonID,
                        Name = value.Name,
                        BirthDate = value.BirthDate,
                        HasCriminalRecord = value.HasCriminalRecord,
                        IncomeInUSD = value.IncomeInUSD,
                        SettlementID = value.SettlementID,
                        CitizenshipID = value.CitizenshipID
                    };
                    OnPropertyChanged();
                }
                ((RelayCommand)CreateCitizenCommand).NotifyCanExecuteChanged();
                ((RelayCommand)DeleteCitizenCommand).NotifyCanExecuteChanged();
                ((RelayCommand)UpdateCitizenCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateCitizenCommand { get; set; }
        public ICommand DeleteCitizenCommand { get; set; }
        public ICommand UpdateCitizenCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CitizenWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Citizens = new RestCollection<Citizen>("http://localhost:54726/", "citizen", "hub");
                Settlements = new RestCollection<Settlement>("http://localhost:54726/", "settlement", "hub");
                Countries = new RestCollection<Country>("http://localhost:54726/", "country", "hub");


                CreateCitizenCommand = new RelayCommand(() =>
                {
                    Citizens.Add(new Citizen()
                    {
                        Name = SelectedCitizen.Name,
                        BirthDate = SelectedCitizen.BirthDate,
                        CitizenshipID = SelectedCitizen.CitizenshipID,
                        HasCriminalRecord = SelectedCitizen.HasCriminalRecord,
                        SettlementID = SelectedCitizen.SettlementID,
                        IncomeInUSD = SelectedCitizen.IncomeInUSD,
                    });
                });

                UpdateCitizenCommand = new RelayCommand(() =>
                {
                    Citizens.Update(SelectedCitizen);
                },
                () =>
                {
                    return SelectedCitizen != null;
                });

                DeleteCitizenCommand = new RelayCommand(() =>
                {
                    Citizens.Delete(SelectedCitizen.PersonID);
                },
                () =>
                {
                    return SelectedCitizen != null;
                });

                //ha kijelölés nélkül hozunk létre, ne fusson hibára - így viszont nem működik, hogy ne legyen inaktív a delete/update gomb, ha semmi sincs kiválasztva, hiszen minimum egy default
                //ki lesz választva
                SelectedCitizen = new Citizen();
            }
        }
    }
}
