using Backend.Data;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Shared_ViewModels.Order;
using Shared_ViewModels.Payment;
using System.Threading.Tasks;
using System.Linq;

namespace Backend.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order?> CreateAsync(AppUser appUser, CreatePaymentRequestVmDto paymentDto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.AppUserId == appUser.Id);

            if (cart == null)
            {
                return null;
            }

            var orderModel = cart.ToOrderFromCart(appUser, paymentDto);

            // Delete Old Cart And Create new Order
            _context.Carts.Remove(cart);
            await _context.Orders.AddAsync(orderModel);

            return orderModel;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (orderModel == null)
            {
                return null;
            }
            _context.Orders.Remove(orderModel);
            return orderModel;
        }

        public async Task<List<Order>> GetAllAsync(AppUser appUser)
        {
            return await _context.Orders
                .Include(c => c.OrderItems)
                .Include(o => o.Payment)
                .Where(c => c.AppUserId == appUser.Id)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(c => c.OrderItems)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> OrderExists(int id)
        {
            return _context.Orders.AnyAsync(s => s.Id == id);
        }

        public async Task<Order?> UpdateAsync(int id, UpdateOrderVmDto orderDto)
        {
            var existingOrder = await _context.Orders
                .Include(c => c.OrderItems)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.Status = orderDto.Status;
            existingOrder.Payment.PaymentMethod = orderDto.Payment.PaymentMethod;

            return existingOrder;
        }
    }
}
