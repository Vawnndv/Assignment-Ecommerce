using Backend.Data;
using Backend.Interfaces;
using Backend.Repository;

namespace Backend.UnitOfWork.ProductRating
{
    public class ProductRatingUnitOfWork : IProductRatingUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private IProductRatingRepository _productRatingRepository;

        public ProductRatingUnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public IProductRatingRepository ProductRatingRepository
        {
            get
            {
                if (_productRatingRepository == null)
                {
                    _productRatingRepository = new ProductRatingRepository(_context);
                }
                return _productRatingRepository;
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
