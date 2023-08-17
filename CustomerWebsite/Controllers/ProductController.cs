using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CustomerWebsite.Models;
using Application.Billing.Services;
using Shared.Contracts.Response;
using Shared.Contracts.Request;
using Application.Token.Services;

namespace CustomerWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
            
        }

        #region Get products
        public async Task<IActionResult> Index()
        {
            return View(await productService.GetProductsAsync());
        }
        #endregion

        #region Login/Logout

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Product")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
        //test functionalities
        //public async Task<ActionResult<OrderResponseDto>> Test()
        //{
        //    TokenRequestDto token = new TokenRequestDto
        //    {
        //        TokenId = "test123",
        //        PaymentProductId = 1,
        //        CardNumber = "XXXXXXXX1111",
        //        ExpiryDate = "1225",
        //        CustomerAuth0UserId= "auth0|63029c4e95ed75a8a6b2343b"
        //    };
        //    var tokenResponse = await tokenService.CreateTokenAsync(token);
        //    return View(tokenResponse);
        //}
    }
}