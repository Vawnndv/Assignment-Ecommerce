using Ecommerce_Customers_Site.Handlers;
using Ecommerce_Customers_Site.Services.Account;
using Ecommerce_Customers_Site.Services.Cart;
using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ecommerce_Customers_Site.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUri = new Uri(configuration["ApiSettings:BaseUri"]);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<AuthTokenHandler>();

            services.AddHttpClient<ICategoryAPIService, CategoryAPIService>(c =>
                c.BaseAddress = baseUri).AddHttpMessageHandler<AuthTokenHandler>();
            services.AddHttpClient<IProductAPIService, ProductAPIService>(c =>
                c.BaseAddress = baseUri).AddHttpMessageHandler<AuthTokenHandler>();
            services.AddHttpClient<IAccountAPIService, AccountAPIService>(c =>
                c.BaseAddress = baseUri).AddHttpMessageHandler<AuthTokenHandler>();
            services.AddHttpClient<ICartAPIService, CartAPIService>(c =>
                c.BaseAddress = baseUri).AddHttpMessageHandler<AuthTokenHandler>();

            return services;
        }
    }
}
