using Backend.Data;
using Backend.Interfaces;
using Backend.Repository;

namespace Backend.UnitOfWork.Product
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private IProductRepository _productRepository;

        public ProductUnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

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
