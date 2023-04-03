using Shopping.App.ViewModel;

namespace Shopping.App.Views;

public partial class SignInPage : ContentPage
{
	public SignInPage(SignInViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}