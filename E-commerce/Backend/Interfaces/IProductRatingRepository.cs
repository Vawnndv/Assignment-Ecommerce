using Backend.Models;
using Shared_ViewModels.Category;
using Shared_ViewModels.Product;

namespace Backend.Interfaces
{
    public interface IProductRatingRepository
    {
        Task<List<ProductRating>> GetAllByProductIdAsync(int id);
        Task<ProductRating?> GetByIdAsync(int id);
        Task<ProductRating> CreateAsync(ProductRating productRatingModel);
        Task<ProductRating?> UpdateAsync(string userId, int id, UpdateProductRatingVmDto productRatingDto);
        Task<ProductRating?> DeleteAsync(int id);
        Task<bool> ProductRatingExists(int id);
    }
}
