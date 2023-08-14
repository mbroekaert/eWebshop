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

namespace CustomerWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IBillingService billingService;
        private readonly IOrderService orderService;

        public ProductController(IProductService productService, IBillingService billingService, IOrderService orderService)
        {
            this.productService = productService;
            this.billingService = billingService;
            this.orderService = orderService;
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
        //    OrderResponseDto order = new OrderResponseDto
        //    {
        //        OrderId = 123,
        //        OrderReference = "02c9c887-1676-4cb8-aa50-c7977857653a",
        //        OrderAmount = 15,
        //        OrderDate = DateTime.Now,
        //        CustomerAuth0UserId = "auth0|63029c4e95ed75a8a6b2343b",
        //        ShippingAddressId = 1,
        //        BillingAddressId = 1,
        //        Status = "Paid"
        //    };

        //    var paymentResult = await orderService.UpdateOrderStatus(order, "Paid");
        //    return View();
        //}
    }
}