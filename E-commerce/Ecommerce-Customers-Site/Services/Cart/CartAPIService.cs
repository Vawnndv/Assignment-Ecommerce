using Shared_ViewModels.Helpers;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Product;
using Ecommerce_Customers_Site.Helpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Linq;

namespace Ecommerce_Customers_Site.Services.Cart
{
    public class CartAPIService : ICartAPIService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/cart";

        public CartAPIService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartVmDto?> GetAll()
        {
            var response = await _client.GetAsync(BasePath);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return await response.ReadContentAsync<CartVmDto>();
        }

        public async Task<CartVmDto> GetById(int id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");

            return await response.ReadContentAsync<CartVmDto>();
        }

        public async Task<CartVmDto> Create(CreateCartRequestVmDto cart)
        {
            var response = await _client.PostAsJsonAsync(BasePath, cart);

            return await response.ReadContentAsync<CartVmDto>();
        }

        public async Task<CartVmDto> Update(UpdateCartVmDto cart)
        {
            var response = await _client.PutAsJsonAsync(BasePath, cart);

            return await response.ReadContentAsync<CartVmDto>();
        }

        public async Task<CartVmDto> Delete()
        {
            var response = await _client.DeleteAsync(BasePath);

            return await response.ReadContentAsync<CartVmDto>();
        }

        public async Task AddToCart(CreateCartRequestVmDto cartItem)
        {
            var cart = await GetAll();
            if (cart == null)
            {
                await Create(cartItem);
            }
            else
            {
                // Update the cart items to merge items with the same ProductId
                var existingItems = cart.CartItems.ToDictionary(item => item.ProductId, item => new UpdateCartItemVmDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });

                foreach (var newItem in cartItem.CartItems)
                {
                    if (existingItems.ContainsKey(newItem.ProductId))
                    {
                        existingItems[newItem.ProductId].Quantity += newItem.Quantity;
                    }
                    else
                    {
                        existingItems[newItem.ProductId] = new UpdateCartItemVmDto
                        {
                            Id = 0, // New item, Id will be assigned by the server
                            ProductId = newItem.ProductId,
                            Quantity = newItem.Quantity,
                            UnitPrice = newItem.UnitPrice
                        };
                    }
                }

                var updateCartVmDto = new UpdateCartVmDto
                {
                    CartItems = existingItems.Values.ToList()
                };

                await Update(updateCartVmDto);
            }
        }

        public async Task<int> GetCartItemCount()
        {
            var cart = await GetAll();
            return cart?.CartItems.Count ?? 0;
        }

        public async Task UpdateCart(int productId, int change)
        {
            var cart = await GetAll();
            var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += change;

                if (existingItem.Quantity <= 0)
                {
                    cart.CartItems.Remove(existingItem);
                }

                var updateCartDto = new UpdateCartVmDto
                {
                    CartItems = cart.CartItems.Select(item => new UpdateCartItemVmDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList()
                };

                await Update(updateCartDto);
            }
        }
    }
}
