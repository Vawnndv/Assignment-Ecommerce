using Backend.Models;
using Shared_ViewModels.Product;
using Shared_ViewModels.Helpers;

namespace Backend.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>?> GetByCategoryAsync(int id, QueryObject query);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id, UpdateProductVmDto productDto);
        Task<Product?> DeleteAsync(int id);
        Task<bool> ProductExists(int id);
    }
}
