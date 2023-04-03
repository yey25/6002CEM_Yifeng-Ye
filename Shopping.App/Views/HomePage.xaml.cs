using Shopping.App.ViewModel;

namespace Shopping.App.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}