using Ecommerce_Customers_Site.Helpers;
using Shared_ViewModels.Category;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Services.Product
{
    public class ProductAPIService : IProductAPIService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/product";

        public ProductAPIService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IList<ProductVmDto>> GetByCategoryId(int id)
        {
            var response = await _client.GetAsync(BasePath + "/category/" + id);

            return await response.ReadContentAsync<List<ProductVmDto>>();
        }
    }
}
