using Ecommerce_Customers_Site.Extensions;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCustomServices(builder.Configuration);

// Register to use in View Component
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use custom error handling middleware
app.UseCustomMiddleware();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.ConfigureRouting();
});

app.Run();
