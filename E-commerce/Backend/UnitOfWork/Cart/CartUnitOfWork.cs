using Backend.Data;
using Backend.Interfaces;
using Backend.Repository;

namespace Backend.UnitOfWork.Cart
{
    public class CartUnitOfWork : ICartUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private CartRepository _cartRepository;

        public CartUnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public ICartRepository CartRepository => _cartRepository ??= new CartRepository(_context);

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
