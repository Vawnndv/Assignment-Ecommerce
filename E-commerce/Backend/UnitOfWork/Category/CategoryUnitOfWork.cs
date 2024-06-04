using Backend.Data;
using Backend.Interfaces;
using Backend.Repository;

namespace Backend.UnitOfWork.Category
{
    public class CategoryUnitOfWork : ICategoryUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private CategoryRepository _categoryRepository;

        public CategoryUnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
