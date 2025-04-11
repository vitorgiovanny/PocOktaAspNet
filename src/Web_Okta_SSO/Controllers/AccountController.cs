using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Okta_SSO.Services;
using Web_Okta_SSO.ViewModel;

namespace Web_Okta_SSO.Controllers
{
    public class AccountController : Controller
    {
        private readonly LdapAuthenticationService _ldapService;

        public AccountController(LdapAuthenticationService ldapService)
        {
            _ldapService = ldapService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (_ldapService.Authenticate(model.Username, model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciais inválidas.";
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
