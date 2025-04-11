using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Okta_SSO.Models;
using Web_Okta_SSO.Services;
using Okta.AspNetCore;

namespace Web_Okta_SSO.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LdapAuthenticationService _ldapService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View(); // ou redirect para onde quiser
        }

        [HttpGet]
        public ActionResult Callback()
        {
            // Aqui voc� trata o retorno da autentica��o
            // Ex: recuperar o token ou redirecionar para a p�gina inicial
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
            => View();

        public IActionResult Secure() => View(User.Claims);

        public IActionResult Login()
        {
            var response = Challenge(new AuthenticationProperties { RedirectUri = "/" });

            return response;
        }

        public IActionResult Logout() 
            => SignOut("Cookies", "OpenIdConnect");

    }
}
