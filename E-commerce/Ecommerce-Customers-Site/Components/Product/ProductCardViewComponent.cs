using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Components.Product
{
    public class ProductCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ProductVmDto product)
        {
            return View(product);
        }
    }
}
