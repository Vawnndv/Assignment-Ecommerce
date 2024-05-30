using Ecommerce_Customers_Site.Services.Cart;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Cart;

public class CartController : Controller
{
    private readonly ICartAPIService _cartService;

    public CartController(ICartAPIService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CreateCartItemRequestVmDto cart)
    {
        var cartItem = new CreateCartRequestVmDto
        {
            CartItems = new List<CreateCartItemRequestVmDto>
            {
                new CreateCartItemRequestVmDto
                {
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice
                }
            }
        };

        await _cartService.AddToCart(cartItem);
        int cartCount = await _cartService.GetCartItemCount();
        return Json(new { success = true, cartCount = cartCount });
    }
}
