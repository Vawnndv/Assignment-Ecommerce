using Ecommerce_Customers_Site.Helpers;
using Shared_ViewModels.Category;

namespace Ecommerce_Customers_Site.Services.Category
{
    public class CategoryAPIService : ICategoryAPIService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/category";

        public CategoryAPIService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IList<CategoryVmDto>> GetAll()
        {
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAsync<List<CategoryVmDto>>();
        }
    }
}
