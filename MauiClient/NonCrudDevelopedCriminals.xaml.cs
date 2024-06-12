namespace MauiClient;

public partial class NonCrudDevelopedCriminals : ContentPage
{
    NonCrudDevelopedCriminalsViewModel viewModel;
	public NonCrudDevelopedCriminals()
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}