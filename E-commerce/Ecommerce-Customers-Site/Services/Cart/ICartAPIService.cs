using Shared_ViewModels.Cart;
using Shared_ViewModels.Helpers;

namespace Ecommerce_Customers_Site.Services.Cart
{
    public interface ICartAPIService
    {
        Task<CartVmDto?> GetAll();
        Task<CartVmDto> GetById(int id);
        Task<CartVmDto> Create(CreateCartRequestVmDto cart);
        Task<CartVmDto> Update(UpdateCartVmDto cart);
        Task<CartVmDto> Delete();
        public Task AddToCart(CreateCartRequestVmDto cartItem);
        public Task<int> GetCartItemCount();
    }
}
