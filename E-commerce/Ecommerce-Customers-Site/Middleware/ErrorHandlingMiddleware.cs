using System.Net;

namespace Ecommerce_Customers_Site.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // Check the response status code
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    // Token expired, clear the token cookie and redirect to Home
                    context.Response.Cookies.Delete("token");
                    context.Response.Redirect("/Home");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Token expired, clear the token cookie and redirect to Home
                context.Response.Cookies.Delete("token");
                context.Response.Redirect("/Home");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Something went wrong: {ex.Message}");
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
