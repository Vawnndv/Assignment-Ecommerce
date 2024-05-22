using Shared_ViewModels.Account;

namespace Ecommerce_Customers_Site.Services.Account
{
    public interface IAccountAPIService
    {
        Task<NewUserVmDto> Login(LoginVmDto login);
        Task<NewUserVmDto> Register(RegisterVmDto register);
    }
}
