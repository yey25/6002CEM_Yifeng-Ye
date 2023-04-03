using Shopping.App.Models;
using Shopping.App.ViewModel;

namespace Shopping.App.Views;

[QueryProperty(nameof(Product), "product")]
public partial class ProductPage : ContentPage
{
	
	public ProductViewModel ViewModel
        => BindingContext as ProductViewModel;
    public Product Product
    {
        set => ViewModel.Product = value;
    }

    public ProductPage(ProductViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}