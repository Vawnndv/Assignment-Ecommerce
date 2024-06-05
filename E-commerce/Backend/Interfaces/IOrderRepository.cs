using Backend.Models;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Order;
using Shared_ViewModels.Payment;

namespace Backend.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync(string userId);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> CreateAsync(string userId, CreatePaymentRequestVmDto paymentDto);
        Task<Order?> UpdateAsync(int id, UpdateOrderVmDto orderDto);
        Task<Order?> DeleteAsync(int id);
        Task<bool> OrderExists(int id);
    }
}
