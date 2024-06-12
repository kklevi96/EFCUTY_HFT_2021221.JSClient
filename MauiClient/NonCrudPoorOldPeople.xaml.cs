namespace MauiClient;

public partial class NonCrudPoorOldPeople : ContentPage
{
    NonCrudPoorOldPeopleViewModel viewModel;

    public NonCrudPoorOldPeople()
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}