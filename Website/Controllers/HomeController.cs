using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website.Models;
using Website.Services;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderservice)
        {
            _logger = logger;
            _orderService = orderservice;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetOrdersAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult AccessDenied ()
        {
            TempData["error"] = "Access refused";
            return View();
        }

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }
        [Authorize]
        public async Task Logout ()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}