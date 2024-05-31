using Ecommerce_Customers_Site.Services.Cart;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Components.Cart
{
    public class CartItemViewComponent : ViewComponent
    {
        private readonly IProductAPIService _service;

        public CartItemViewComponent(IProductAPIService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IViewComponentResult> InvokeAsync(CartItemVmDto cartItem)
        {
            var product = await _service.GetById(cartItem.ProductId);
            var productType = product.ProductTypes.FirstOrDefault();
            var productImage = productType?.ProductImages.FirstOrDefault();

            var viewModel = new CartItemDetailVm
            {
                CartItem = cartItem,
                Product = product,
                ProductImageUrl = productImage?.ImageUrl
            };

            return View(viewModel);
        }
    }
}
