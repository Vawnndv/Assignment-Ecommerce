using Shared_ViewModels.Category;

namespace Ecommerce_Customers_Site.Services.Category
{
    public interface ICategoryAPIService
    {
        Task<IList<CategoryVmDto>> GetAll();
    }
}
