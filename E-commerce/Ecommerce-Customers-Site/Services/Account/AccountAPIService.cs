using Ecommerce_Customers_Site.Helpers;
using Shared_ViewModels.Account;
using Shared_ViewModels.Category;

namespace Ecommerce_Customers_Site.Services.Account
{
    public class AccountAPIService : IAccountAPIService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/account";

        public AccountAPIService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<NewUserVmDto> Login(LoginVmDto login)
        {
            var response = await _client.PostAsJsonAsync($"{BasePath}/login", login);

            return await response.ReadContentAsync<NewUserVmDto>();
        }

        public async Task<NewUserVmDto> Register(RegisterVmDto register)
        {
            var response = await _client.PostAsJsonAsync($"{BasePath}/register", register);

            return await response.ReadContentAsync<NewUserVmDto>();
        }
    }
}
