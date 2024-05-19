using Ecommerce_Customers_Site.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Customers_Site.Components.Categories
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryAPIService _service;

        public CategoriesViewComponent(ICategoryAPIService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _service.GetAll();

            return View(categories);
        }
    }
}
