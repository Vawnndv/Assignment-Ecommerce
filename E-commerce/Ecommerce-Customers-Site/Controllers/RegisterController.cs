using Ecommerce_Customers_Site.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Account;

namespace Ecommerce_Customers_Site.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAccountAPIService _accountService;

        public RegisterController(IAccountAPIService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        // GET: /Register/
        public IActionResult Index()
        {
            ViewBag.IsHomeOrAuthPage = true;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterVmDto registerDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Register(registerDto);
                    if (result != null)
                    {
                        // handle success (e.g., set authentication cookie)
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
                        ModelState.AddModelError("", "Registration failed");
                    }
                }

                ViewBag.IsHomeOrAuthPage = true;
                return View(registerDto);

            }
            catch (Exception ex)
            {
                // Handle API
                ViewBag.Error = ex.Message;
                ViewBag.IsHomeOrAuthPage = true;
                return View(registerDto);
            }
        }
    }
}
