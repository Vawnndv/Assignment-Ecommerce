using Backend.Models;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Category;

namespace Backend.Mappers
{
    public static class CartMappers
    {
        public static CartItemVmDto ToCartItemDto(this CartItem cartModel)
        {
            return new CartItemVmDto
            {
                Id = cartModel.Id,
                ProductId = cartModel.ProductId,
                Quantity = cartModel.Quantity,
                UnitPrice = cartModel.UnitPrice
            };
        }

        public static CartVmDto ToCartDto(this Cart cartModel)
        {
            return new CartVmDto
            {
                Id = cartModel.Id,
                CartItems = cartModel.CartItems.Select(s => s.ToCartItemDto()).ToList(),
                TotalAmount = cartModel.TotalAmount
            };
        }

        public static CartItem ToCartItemFromCreateDTO(this CreateCartItemRequestVmDto cartDto)
        {
            return new CartItem
            {
                ProductId = cartDto.ProductId,
                Quantity = cartDto.Quantity,
                UnitPrice = cartDto.UnitPrice
            };
        }
        public static Cart ToCartFromCreateDTO(this CreateCartRequestVmDto cartDto, AppUser appUser)
        {
            return new Cart
            {
                AppUserId = appUser.Id,
                CartItems = cartDto.CartItems.Select(s => s.ToCartItemFromCreateDTO()).ToList(),
                // Calc Total Amount
                TotalAmount = cartDto.CartItems.Sum(item => item.Quantity * item.UnitPrice) 
            };
        }
    }
}
