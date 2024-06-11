namespace MauiClient;
using Model;

public partial class SettlementPage : ContentPage
{
    SettlementViewModel viewModel;
    public SettlementPage(SettlementViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;

        //countryPicker.SelectedIndexChanged += (sender, args) =>
        //{
        //    if (countryPicker.SelectedIndex != -1)
        //    {
        //        var selectedCountryName = (string)countryPicker.SelectedItem;
        //        viewModel.SelectedSettlement.Country.Name = selectedCountryName;
        //    }
        //};
    }
}