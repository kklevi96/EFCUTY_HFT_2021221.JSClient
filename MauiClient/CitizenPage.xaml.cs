namespace MauiClient;

public partial class CitizenPage : ContentPage
{
    CitizenViewModel viewModel;
    public CitizenPage(CitizenViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}