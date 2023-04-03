using Shopping.App.ViewModel;

namespace Shopping.App.Views;

public partial class ShoppingCartPage : ContentPage
{
	public ShoppingCartViewModel ViewModel
		=> BindingContext as ShoppingCartViewModel;

    public ShoppingCartPage(ShoppingCartViewModel viewModel)
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