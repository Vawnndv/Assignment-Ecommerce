using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Shared_ViewModels.Cart;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext _context;

        public CartRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart cartModel)
        {
            await _context.Carts.AddAsync(cartModel);
            return cartModel;
        }

        public async Task<Cart?> DeleteAsync(string userId)
        {
            var cartModel = await _context.Carts.FirstOrDefaultAsync(x => x.AppUserId == userId);
            if (cartModel == null)
            {
                return null;
            }

            _context.Carts.Remove(cartModel);
            return cartModel;
        }

        public async Task<Cart?> GetAllAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> CartExists(int id)
        {
            return _context.Carts.AnyAsync(s => s.Id == id);
        }

        public async Task<Cart?> UpdateAsync(UpdateCartVmDto cartDto, string userId)
        {
            var existingCart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(x => x.AppUserId == userId);

            if (existingCart == null)
            {
                return null;
            }

            // Remove Cart Item not present in the DTO
            foreach (var existingCartItem in existingCart.CartItems.ToList())
            {
                if (!cartDto.CartItems.Any(dto => dto.Id == existingCartItem.Id))
                {
                    // Remove the Cart Item
                    existingCart.CartItems.Remove(existingCartItem);
                }
            }

            foreach (var item in cartDto.CartItems)
            {
                var existingCartItem = existingCart.CartItems.FirstOrDefault(ci => ci.Id == item.Id);
                if (existingCartItem != null)
                {
                    // Update existing Cart Item properties
                    existingCartItem.ProductId = item.ProductId;
                    existingCartItem.Quantity = item.Quantity;
                    existingCartItem.UnitPrice = item.UnitPrice;
                }
                else
                {
                    // Create a new Cart Item
                    var newCartItem = new CartItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };

                    existingCart.CartItems.Add(newCartItem);
                }
            }

            existingCart.TotalAmount = existingCart.CalculateTotalAmount();

            return existingCart;
        }
    }
}
