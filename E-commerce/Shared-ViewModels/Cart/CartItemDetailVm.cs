using Shared_ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Cart
{
    public class CartItemDetailVm
    {
        public CartItemVmDto CartItem { get; set; }
        public ProductVmDto Product { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
