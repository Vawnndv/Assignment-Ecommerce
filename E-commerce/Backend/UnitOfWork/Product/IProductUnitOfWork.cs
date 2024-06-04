using Backend.Interfaces;

namespace Backend.UnitOfWork.Product
{
    public interface IProductUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get;  }
        Task<int> CompleteAsync();
    }

}
