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
    public class ShoppingCartViewModel : BaseViewModel
    {
        private readonly ShoppingCartService _shoppingCartService;

        private readonly OrderService _orderService;

        private readonly UserService _userService;

        #region Property

        public ObservableCollection<CartItem> CartItems { get; set; }


        private double _totalPrice;

        public double TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
        }

        #endregion

        #region Command

        private Command _deleteCommand;

        public Command DeleteCommand
            => _deleteCommand ??= new(Delete);


        private Command _payCommand;

        public Command PayCommand
            => _payCommand ??= new(Pay);

        #endregion  

        public ShoppingCartViewModel(
            ShoppingCartService shoppingCartService,
            OrderService orderService,
            UserService userService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _userService = userService;
            CartItems = new();

            RefreshData();
        }

        public void RefreshData()
        {
            CartItems.Clear();
            foreach(var  cartItem in _shoppingCartService.CartItems) 
            { 
                CartItems.Add(cartItem);
            }

            TotalPrice = _shoppingCartService.TotalPrice;

            foreach (var item in CartItems)
            {
                item.OnQuanityChanged = OnPriceChanged;
                item.OnSelectedChanged = OnPriceChanged;
            }
        }


        private void Delete()
        {
            var deleteItems = CartItems.Where(item => item.IsSelected)
                                        .ToList();
            Delete(deleteItems);
        }

        private void Delete(IList<CartItem> deleteItems)
        {
            foreach (var item in deleteItems)
            {
                CartItems.Remove(item);
                _shoppingCartService.RemoveProduct(item.Product);
            }

            TotalPrice = _shoppingCartService.TotalPrice;
        }

        private void Pay()
        {
            if (!CartItems.Any())
                return;

            if (!_userService.IsLogIn)
            {
                DisplayAlert("Failed","please login first" ,"Cloes");
                return;
            }

            var order = new Order();

            order.UserId = _userService.UserId;
            order.Price = TotalPrice;
            order.OrderDate = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm");

            order.Details = new List<OrderDetail>();
            var selectedItems = CartItems.Where(item => item.IsSelected).ToList();
            foreach (var cartItem in selectedItems)
            {
                order.Details.Add(new OrderDetail()
                {
                    Order = order,
                    Product = cartItem.Product,
                    Quantity = cartItem.Quantity,
                });
            }

            bool succeess = _orderService.SubmitOrder(order);

            if (!succeess)
                DisplayAlert("Failed", "order failed");
            else
            {
                Delete(selectedItems);
                DisplayAlert("Success", "order completed", "OK");
            }
        }

        private void OnPriceChanged(CartItem item)
        {
            TotalPrice = _shoppingCartService.TotalPrice;
        }
    }
}
