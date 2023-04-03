
using Shopping.App.Models;
using Shopping.App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private readonly ShoppingCartService _shoppingCartService;

        private IList<Product> _products { get; set; }

        #region Property

        public ObservableCollection<Product> SearchProducts { get; set; }


        private string _searchText;

        public string SearchText
        {
            get => _searchText; 
            set 
            {
                if (_searchText != value && string.IsNullOrEmpty(value))
                    ClearSearch();

                SetProperty(ref _searchText, value);
            }
        }

        #endregion


        #region Command

        private Command<Product> _productDetailCommand;

        public Command<Product> ProductDetailCommand
            => _productDetailCommand ?? new(ViewProductDetail);

        private Command<Product> _addToCartCommand;

        public Command<Product> AddToCartCommand
            => _addToCartCommand ??= new(AddToCart);


        private Command _searchCommand;

        public Command SearchCommand
            => _searchCommand ??= new(Search);
        #endregion

        public HomeViewModel(
            ProductService productService, 
            ShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;

            _products = _productService.GetProducts();
            SearchProducts = new (_products);
        }


        private async void ViewProductDetail(Product product)
        {
            var nParams = new Dictionary<string, object>()
            {
                { "product", product }
            };

            await Shell.Current.GoToAsync("Product/Detail", nParams);
        }

        private void AddToCart(Product product)
        {
            _shoppingCartService.AddProduct(product);
            DisplayAlert("Success", "add to shopping cart success.", "OK");
        }

        private void Search()
            => UpdateSearchProducts(_products.Where(p => p.Name.Contains(SearchText)));

        private void ClearSearch()
            => UpdateSearchProducts(_products);

        private void UpdateSearchProducts(IEnumerable<Product> products)
        {
            SearchProducts.Clear();
            foreach(var product in products)
            {
                SearchProducts.Add(product);
            }
        }
    }
}
