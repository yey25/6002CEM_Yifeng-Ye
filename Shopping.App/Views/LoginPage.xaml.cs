using Shopping.App.ViewModel;

namespace Shopping.App.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel	viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}