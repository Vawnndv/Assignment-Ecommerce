using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;

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
        public async Task<IActionResult> Collections(int? id, int? page)
        {
            if (id.HasValue)
            {
                var query = new QueryObject
                {
                    PageNumber = page ?? 1, // Assign 1 when page == null
                };
                var totalPages = await _productService.GetNumOfProductPagesByCategory(id.Value, query);

                var products = await _productService.GetByCategoryId(id.Value, query);

                var tuple = new Tuple<IList<ProductVmDto>, int>(products, totalPages);

                ViewBag.CurrentPage = page;
                ViewBag.CategoryId = id.Value;

                return View(tuple);
            }

            return View();
        }
    }
}
