using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Shared_ViewModels.Category;

namespace Backend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryModel == null)
            {
                return null;
            }
            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.SubCategories).Where(c => c.ParentCategoryId == null).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.SubCategories).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> CategoryExists(int id)
        {
            return _context.Categories.AnyAsync(s => s.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryVmDto categoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description;

            await _context.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<List<Category>> GetParentCategoriesAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            var parentCategories = new List<Category>();
            
            if (category != null)
            {
                parentCategories.Add(category);
            }

            while (category?.ParentCategoryId != null)
            {
                category = await _context.Categories.FindAsync(category.ParentCategoryId);
                if (category != null)
                {
                    parentCategories.Insert(0, category);
                }
            }

            return parentCategories;
        }
    }
}
