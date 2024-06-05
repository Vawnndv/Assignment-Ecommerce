using Backend.Models;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Category;

namespace Backend.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetAllAsync(string userId);
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart> CreateAsync(Cart cartModel);
        Task<Cart?> UpdateAsync(UpdateCartVmDto cartDto, string userId);
        Task<Cart?> DeleteAsync(string userId);
        Task<bool> CartExists(int id);
    }
}
