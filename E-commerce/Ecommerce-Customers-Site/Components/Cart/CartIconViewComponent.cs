using Ecommerce_Customers_Site.Services.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ecommerce_Customers_Site.Components.Cart
{
    public class CartIconViewComponent : ViewComponent
    {
        private readonly ICartAPIService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartIconViewComponent(ICartAPIService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (!httpContext.Request.Cookies.TryGetValue("token", out var token))
            {
                return View(0);
            }

            var numItems = await _service.GetCartItemCount();

            return View(numItems);
        }
    }
}
