namespace MauiClient;

public partial class SettlementPage : ContentPage
{
    SettlementViewModel viewModel;
    public SettlementPage(SettlementViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}