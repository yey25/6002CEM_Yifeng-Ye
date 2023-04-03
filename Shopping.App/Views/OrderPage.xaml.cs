using Shopping.App.Models;
using Shopping.App.ViewModel;

namespace Shopping.App.Views;

[QueryProperty(nameof(Order), "order")]
public partial class OrderPage : ContentPage
{
	public OrderViewModel ViewModel
		=> BindingContext as OrderViewModel;
	
	public Order Order
	{
		set => ViewModel.Order = value;
	}

	public OrderPage(OrderViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}