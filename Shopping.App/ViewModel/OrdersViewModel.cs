using Microsoft.Maui.Controls;
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
    public class OrdersViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;

        private readonly UserService _userService;

        public ObservableCollection<Order> Orders { get; set; }


        private Command<Order> _orderDetailCommand;

        public Command<Order> OrderDetailCommand
            => _orderDetailCommand ??= new(OrderDetail);

        public OrdersViewModel(
            OrderService orderService, 
            UserService userService)
        {
            _orderService = orderService;
            _userService = userService;
            Orders = new();

            RefreshData();
        }

        public void RefreshData()
        {
            Orders.Clear();
            var userId = _userService.UserId;
            if (userId <= 0)
                return;

            foreach(var item in _orderService.GetOrderByUserId(userId))
            {
                Orders.Add(item);
            }
        }


        private async void OrderDetail(Order order)
        {
            var nParams = new Dictionary<string, object>
            {
                { "order", order}
            };

            await Shell.Current.GoToAsync("Order/Detail", nParams);
        }
    }
}
