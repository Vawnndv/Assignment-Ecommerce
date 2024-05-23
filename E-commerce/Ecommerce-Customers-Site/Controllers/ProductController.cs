using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared_ViewModels.Account;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductAPIService _productService;

        public ProductController(IProductAPIService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // GET: /Product/
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: /Product/Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id.HasValue)
            {
                var product = await _productService.GetById(id.Value);

                return View(product);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review(int? ProductId, int Rating, string Review)
        {
            if (ProductId.HasValue)
            {

                if (ModelState.IsValid)
                {
                    var result = await _productService.Review(new CreateProductRatingRequestVmDto
                    {
                        ProductId = ProductId.Value,
                        Rating = Rating,
                        Review = Review
                    });

                    if (result != null)
                    {
                        return RedirectToAction("Detail", new { id = ProductId.Value }); // Redirect to product detail page
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to submit review");
                    }
                }

                var product = await _productService.GetById(ProductId.Value);
                return View("Detail", product);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
