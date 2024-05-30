using Backend.Models;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Category;

namespace Backend.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetAllAsync(AppUser appUser);
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart> CreateAsync(Cart cartModel);
        Task<Cart?> UpdateAsync(UpdateCartVmDto cartDto, AppUser appUser);
        Task<Cart?> DeleteAsync(AppUser appUser);
        Task<bool> CartExists(int id);
    }
}
