using Ecommerce_Customers_Site.Helpers;
using Shared_ViewModels.Account;
using Shared_ViewModels.Category;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;

namespace Ecommerce_Customers_Site.Services.Product
{
    public class ProductAPIService : IProductAPIService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/product";
        public const string BasePathRating = "/api/productrating";

        public ProductAPIService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IList<ProductVmDto>> GetByCategoryId(int id, QueryObject query)
        {
            var response = await _client.GetAsync($"{BasePath}/category/{id}?{QueryStringHelper.ToQueryString(query)}");

            return await response.ReadContentAsync<List<ProductVmDto>>();
        }

        public async Task<IList<ProductVmDto>> GetAll(QueryObject query)
        {
            var response = await _client.GetAsync($"{BasePath}?{QueryStringHelper.ToQueryString(query)}");

            return await response.ReadContentAsync<List<ProductVmDto>>();
        }

        public async Task<int> GetNumOfProductPagesByCategory(int id, QueryObject query)
        {
            var response = await _client.GetAsync($"{BasePath}/category/numberpages/{id}?{QueryStringHelper.ToQueryString(query)}");

            return await response.ReadContentAsync<int>();
        }

        public async Task<ProductVmDto> GetById(int id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");

            return await response.ReadContentAsync<ProductVmDto>();
        }

        public async Task<ProductRatingVmDto> Review(CreateProductRatingRequestVmDto review)
        {
            var response = await _client.PostAsJsonAsync(BasePathRating, review);

            return await response.ReadContentAsync<ProductRatingVmDto>();
        }
    }
}
