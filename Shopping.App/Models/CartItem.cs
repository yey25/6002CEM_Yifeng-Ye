using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.Models
{
    public class CartItem
    {
        private bool _isSeleced = true;
        public bool IsSelected
        {
            get => _isSeleced;
            set
            {
                if (value == _isSeleced)
                    return;
                
                _isSeleced = value;
                OnSelectedChanged?.Invoke(this);
            }
        }

        public Product Product { get; set; }

        private int _quantity = 1;

        public int Quantity 
        {
            get => _quantity;
            set
            {
                if (value == _quantity)
                    return;

                _quantity = value;
                OnQuanityChanged?.Invoke(this);
            }
        }

        public Action<CartItem> OnQuanityChanged { get; set; }

        public Action<CartItem> OnSelectedChanged { get; set; }
    }
}
