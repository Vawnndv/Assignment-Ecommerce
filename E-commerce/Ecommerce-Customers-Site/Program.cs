using Ecommerce_Customers_Site.Services.Category;
using Ecommerce_Customers_Site.Services.Product;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var baseUri = new Uri(builder.Configuration["ApiSettings:BaseUri"]);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ICategoryAPIService, CategoryAPIService>(c =>
c.BaseAddress = baseUri);
builder.Services.AddHttpClient<IProductAPIService, ProductAPIService>(c =>
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
