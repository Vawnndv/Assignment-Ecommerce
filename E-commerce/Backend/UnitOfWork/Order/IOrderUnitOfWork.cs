using Backend.Interfaces;

namespace Backend.UnitOfWork.Order
{
    public interface IOrderUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        Task<int> CompleteAsync();
    }
}
