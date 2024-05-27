using Backend.Models;
using Shared_ViewModels.Category;

namespace Backend.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, UpdateCategoryVmDto categoryDto);
        Task<Category?> DeleteAsync(int id);
        Task<bool> CategoryExists(int id);
        Task<List<Category>> GetParentCategoriesAsync(int categoryId);
    }
}
