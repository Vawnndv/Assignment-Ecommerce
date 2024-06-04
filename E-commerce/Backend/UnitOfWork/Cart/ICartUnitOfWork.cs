using Backend.Interfaces;

namespace Backend.UnitOfWork.Cart
{
    public interface ICartUnitOfWork : IDisposable
    {
        ICartRepository CartRepository { get; }
        Task<int> CompleteAsync();
    }
}
