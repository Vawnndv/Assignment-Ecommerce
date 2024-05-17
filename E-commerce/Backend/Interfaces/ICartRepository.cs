using Backend.Models;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Category;

namespace Backend.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllAsync(AppUser appUser);
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart> CreateAsync(Cart cartModel);
        Task<Cart?> UpdateAsync(int id, UpdateCartVmDto cartDto);
        Task<Cart?> DeleteAsync(int id);
        Task<bool> CartExists(int id);
    }
}
