using Shopping.App.Models;
using Shopping.App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;

        private Order _order;

        public Order Order 
        {
            get => _order;
            set => SetProperty(ref _order, value); 
        }

        public OrderViewModel(OrderService orderService) 
        {
            _orderService = orderService;
        }
    }
}
