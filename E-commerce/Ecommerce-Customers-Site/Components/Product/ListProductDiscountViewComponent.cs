using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Components.Product
{
    public class ListProductDiscountViewComponent : ViewComponent
    {
        private readonly IProductAPIService _productService;

        public ListProductDiscountViewComponent(IProductAPIService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAll(new QueryObject
            {
                IsDiscount = true,
            });

            return View(products);
        }
    }
}
