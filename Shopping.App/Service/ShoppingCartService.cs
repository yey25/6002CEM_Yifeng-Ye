
using Shopping.App.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.Service
{
    public class ShoppingCartService
    {
        //private static Lazy<ShoppingCartService> _shopingCartService
        //        = new Lazy<ShoppingCartService>(() => new ShoppingCartService());

        //public static ShoppingCartService Instance => _shopingCartService.Value;

        public EventHandler<CartItem> OnAddCartItem;

        public EventHandler<CartItem> OnRemoveCartItem;

        // <productId, cartItem>
        private Dictionary<int, CartItem> _cartItems;

        public IList<CartItem> CartItems
            => _cartItems.Select(p => p.Value)
                         .ToList();

        public double TotalPrice => 
            _cartItems.Where(p => p.Value.IsSelected)
                .Sum(p => p.Value.Quantity * p.Value.Product.Price);


        public ShoppingCartService()
        {
            _cartItems = new();
        }


        private bool TryAddNewCartItem(Product product)
        {
            if (_cartItems.ContainsKey(product.Id))
                return false;

            _cartItems[product.Id] = new CartItem()
            {
                IsSelected = true,
                Product = product,
                Quantity = 0,
            };

            OnAddCartItem?.Invoke(this, _cartItems[product.Id]);
            return true;
        }

        public void AddProduct(Product product, int quantity = 1)
        {
            TryAddNewCartItem(product);
            _cartItems[product.Id].Quantity += quantity;
        }



        public void ModifyProductQuantity(Product product, int quantity = 1)
        {
            TryAddNewCartItem(product);

            if (quantity <= 0)
                RemoveProduct(product);
            else
                _cartItems[product.Id].Quantity = quantity;
        }


        public void RemoveProduct(Product product)
        {
            if(_cartItems.Remove(product.Id, out var item))
                OnRemoveCartItem?.Invoke(this, item);
        }


        public void Clear()
        {
            _cartItems.Clear();
        }

    }

    public enum CartItemOperation
    {
        Add,
        Remove,
    }
}
