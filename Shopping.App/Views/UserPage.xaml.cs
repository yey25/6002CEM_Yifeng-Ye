using Shopping.App.ViewModel;

namespace Shopping.App.Views;

public partial class UserPage : ContentPage
{
	public UserViewModel ViewModel 
		=> BindingContext as UserViewModel;

    public UserPage(UserViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.RefreshData();
    }
}