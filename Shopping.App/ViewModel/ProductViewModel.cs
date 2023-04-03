
using Shopping.App.Models;
using Shopping.App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {

        private readonly ShoppingCartService _shoppingCartService;
        private readonly ProductService _productService;


        #region Property

        private Product _product;

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }


        private int _quantity = 1;

        public int Quantity 
        { 
            get => _quantity; 
            set => SetProperty(ref _quantity, value); 
        }

        #endregion


        private Command _addToCartCommand;

        public Command AddToCartCommand
            => _addToCartCommand ??= new(AddToCart);

        public ProductViewModel(
            ShoppingCartService shoppingCartService,
            ProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }


        private void AddToCart()
        {
            _shoppingCartService.AddProduct(Product, Quantity);
            DisplayAlert("Success", "add to shopping cart success.", "OK");
        }
    }
}
