using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EFCUTY_HFT_2021221.WPFClient.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand OpenCitizenWindowCommand { get; set; }
        public ICommand OpenSettlementWindowCommand { get; set; }
        public ICommand OpenCountryWindowCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                OpenCitizenWindowCommand = new RelayCommand(() =>
                {
                    CitizenWindow citizenWindow = new();
                    citizenWindow.Show();
                });
                OpenSettlementWindowCommand = new RelayCommand(() =>
                {
                    SettlementWindow settlementWindow = new();
                    settlementWindow.Show();
                });
                OpenCountryWindowCommand = new RelayCommand(() =>
                {
                    CountryWindow countryWindow = new();
                    countryWindow.Show();
                });
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
