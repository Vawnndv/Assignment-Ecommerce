using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Helpers;

namespace Ecommerce_Customers_Site.Components.Product
{
    public class ListProductNewViewComponent : ViewComponent
    {
        private readonly IProductAPIService _productService;

        public ListProductNewViewComponent(IProductAPIService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAll(new QueryObject
            {
                IsLatest = true,
            });

            return View(products);
        }
    }
}
