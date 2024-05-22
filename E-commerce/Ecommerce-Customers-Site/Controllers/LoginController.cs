using Ecommerce_Customers_Site.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Account;

namespace Ecommerce_Customers_Site.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountAPIService _accountService;

        public LoginController(IAccountAPIService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        // GET: /Login/
        public IActionResult Index()
        {
            ViewBag.IsHomeOrAuthPage = true;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVmDto loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Login(loginDto);
                    if (result != null)
                    {
                        // handle success (e.g., set authentication cookie)
                        string token = result.Token;
                        Response.Cookies.Append("token", token, new CookieOptions
                        {
                            HttpOnly = true, // Only allow cookies to be accessed from the server side
                            Secure = true, // Only send cookies over HTTPS if HTTPS is being used
                            SameSite = SameSiteMode.Strict // Prevent cookies from being sent from external websites
                        });

                        return RedirectToAction("Index", "Home"); // Redirect to home or any other page
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login failed");
                    }
                }

                ViewBag.IsHomeOrAuthPage = true;

                return View(loginDto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.IsHomeOrAuthPage = true;
                return View(loginDto);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Remove the token cookie
            Response.Cookies.Delete("token");

            // Optionally, redirect to the login page or home page
            return RedirectToAction("Index", "Home");
        }
    }
}
