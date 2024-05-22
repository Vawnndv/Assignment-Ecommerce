using Ecommerce_Customers_Site.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Account;

namespace Ecommerce_Customers_Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountAPIService _accountService;

        public AccountController(IAccountAPIService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVmDto loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Login(loginDto);
                    if (result != null)
                    {
                        // handle success (e.g., set authentication cookie)
                        return RedirectToAction("Index", "Home"); // Redirect to home or any other page
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login failed");
                    }
                }

                return PartialView("Login", loginDto); // Return to the ViewComponent with validation messages
            }
            catch (Exception ex)
            {
                // Handle API
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVmDto registerDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Register(registerDto);
                    if (result != null)
                    {
                        // handle success (e.g., set authentication cookie)
                        return RedirectToAction("Index", "Home"); // Redirect to home or any other page
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed");
                    }
                }

                return ViewComponent("Register", registerDto); // Return to the ViewComponent with validation messages

            }
            catch (Exception ex)
            {
                // Handle API
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }
    }
}
