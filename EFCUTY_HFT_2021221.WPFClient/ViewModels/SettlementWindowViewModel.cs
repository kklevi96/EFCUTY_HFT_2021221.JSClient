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
    public class SettlementWindowViewModel : ObservableRecipient
    {
        public RestCollection<Settlement> Settlements { get; set; }
        public RestCollection<Country> Countries { get; set; }

        private Settlement selectedSettlement;

        public Settlement SelectedSettlement
        {
            get { return selectedSettlement; }
            set
            {
                if (value != null)
                {
                    selectedSettlement = new Settlement()
                    {
                        SettlementID=value.SettlementID,
                        SettlementName = value.SettlementName,
                        Population = value.Population,
                        HDI = value.HDI,
                        CountryID = value.CountryID
                        //Settlements = value.Settlements,
                        //Citizens = value.Citizens
                    };
                    OnPropertyChanged();
                }
                ((RelayCommand)CreateSettlementCommand).NotifyCanExecuteChanged();
                ((RelayCommand)DeleteSettlementCommand).NotifyCanExecuteChanged();
                ((RelayCommand)UpdateSettlementCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateSettlementCommand { get; set; }
        public ICommand DeleteSettlementCommand { get; set; }
        public ICommand UpdateSettlementCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public SettlementWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Settlements = new RestCollection<Settlement>("http://localhost:54726/", "settlement", "hub");
                Countries = new RestCollection<Country>("http://localhost:54726/", "country", "hub");

                CreateSettlementCommand = new RelayCommand(() =>
                {
                    Settlements.Add(new Settlement()
                    {
                        SettlementName = SelectedSettlement.SettlementName,
                        HDI = SelectedSettlement.HDI,
                        Population = SelectedSettlement.Population,
                        CountryID = SelectedSettlement.CountryID

                    });
                });

                UpdateSettlementCommand = new RelayCommand(() =>
                {
                    Settlements.Update(SelectedSettlement);
                },
                () =>
                {
                    return SelectedSettlement != null;
                });

                DeleteSettlementCommand = new RelayCommand(() =>
                {
                    Settlements.Delete(SelectedSettlement.SettlementID);
                },
                () =>
                {
                    return SelectedSettlement != null;
                });

                //ha kijelölés nélkül hozunk létre, ne fusson hibára - így viszont nem működik, hogy ne legyen inaktív a delete/update gomb, ha semmi sincs kiválasztva, hiszen minimum egy default
                //ki lesz választva
                SelectedSettlement = new Settlement();
            }
        }
    }
}
