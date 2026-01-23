using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart
{
    public  class VmGetCartItem
    {
        public Guid CartItemId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
