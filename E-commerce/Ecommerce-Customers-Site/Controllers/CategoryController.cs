using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Customers_Site.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryAPIService _categoryService;
        private readonly IProductAPIService _productService;


        public CategoryController(ICategoryAPIService categoryService, IProductAPIService productService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // GET: /Category/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Category/Collections/{?id}
        public async Task<IActionResult> Collections(int? id)
        {
            if (id.HasValue)
            {
                var products = await _productService.GetByCategoryId(id.Value);
                return View(products);
            }

            return View();
        }
    }
}
