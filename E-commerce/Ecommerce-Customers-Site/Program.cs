using Ecommerce_Customers_Site.Handlers;
using Ecommerce_Customers_Site.Middleware;
using Ecommerce_Customers_Site.Services.Account;
using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Register IHttpContextAccessor and AuthTokenHandler
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<AuthTokenHandler>();

// Add services to the container.
var baseUri = new Uri(builder.Configuration["ApiSettings:BaseUri"]);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ICategoryAPIService, CategoryAPIService>(c =>
c.BaseAddress = baseUri);
builder.Services.AddHttpClient<IProductAPIService, ProductAPIService>(c =>
c.BaseAddress = baseUri);
builder.Services.AddHttpClient<IAccountAPIService, AccountAPIService>(c =>
c.BaseAddress = baseUri);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use custom error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
