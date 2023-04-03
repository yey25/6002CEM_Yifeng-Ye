using Shopping.App.ViewModel;

namespace Shopping.App.Views;

public partial class OrdersPage : ContentPage
{
	public OrdersViewModel ViewModel
		=> BindingContext as OrdersViewModel;

    public OrdersPage(OrdersViewModel viewModel)
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