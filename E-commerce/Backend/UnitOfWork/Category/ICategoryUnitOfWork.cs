using Backend.Interfaces;

namespace Backend.UnitOfWork.Category
{
    public interface ICategoryUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        Task<int> CompleteAsync();
    }
}
