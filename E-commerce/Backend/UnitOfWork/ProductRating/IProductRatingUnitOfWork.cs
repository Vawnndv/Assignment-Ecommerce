using Backend.Interfaces;

namespace Backend.UnitOfWork.ProductRating
{
    public interface IProductRatingUnitOfWork : IDisposable
    {
        IProductRatingRepository ProductRatingRepository { get; }
        Task<int> CompleteAsync();
    }
}
