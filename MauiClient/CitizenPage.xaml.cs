namespace MauiClient;

public partial class CitizenPage : ContentPage
{
    CitizenViewModel viewModel;
    public CitizenPage(CitizenViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;

        //countryPicker.SelectedIndexChanged += (sender, args) =>
        //{
        //    if (countryPicker.SelectedIndex != -1)
        //    {
        //        var selectedCountry = (Country)countryPicker.SelectedItem;
        //        viewModel.SelectedSettlement.CountryID = selectedCountry.CountryID;
        //        viewModel.SelectedSettlement.Country.Name = selectedCountry.Name;
        //    }
        //};
    }
}