using Backend.Data;
using Backend.Interfaces;
using Backend.Repository;

namespace Backend.UnitOfWork.Order
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private IOrderRepository _orderRepository;

        public OrderUnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);

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
