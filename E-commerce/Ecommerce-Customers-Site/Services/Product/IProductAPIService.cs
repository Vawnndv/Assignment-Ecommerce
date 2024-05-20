using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Services.Product
{
    public interface IProductAPIService
    {
        Task<IList<ProductVmDto>> GetByCategoryId(int id);
        Task<IList<ProductVmDto>> GetAll(QueryObject query);
    }
}
