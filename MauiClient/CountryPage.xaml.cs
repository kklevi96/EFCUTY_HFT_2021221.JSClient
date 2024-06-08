namespace MauiClient;

public partial class CountryPage : ContentPage
{
	CountryViewModel viewModel;
	public CountryPage(CountryViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}
}