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
        public async Task<IActionResult> Collections(int? id, int? page, string? sortBy, string? isDescending, int minPrice = int.MinValue, int maxPrice = int.MaxValue)
        {
            if (id.HasValue)
            {
                var query = new QueryObject
                {
                    PageNumber = page ?? 1, // Assign 1 when page == null
                    SortBy = sortBy,
                    IsDecsending = isDescending == "on" ? true : false,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };

                var totalPages = await _productService.GetNumOfProductPagesByCategory(id.Value, query);

                var products = await _productService.GetByCategoryId(id.Value, query);

                var tuple = new Tuple<IList<ProductVmDto>, int>(products, totalPages);

                ViewBag.CurrentPage = page ?? 1;
                ViewBag.CategoryId = id.Value;
                ViewBag.Sortby = sortBy;
                ViewBag.IsDescending = isDescending;
                ViewBag.MinPrice = minPrice;
                ViewBag.MaxPrice = maxPrice;

                return View(tuple);
            }

            return View();
        }
    }
}
