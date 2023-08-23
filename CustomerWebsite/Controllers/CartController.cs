using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.Contracts.Request;
using Shared.Contracts.Response;

namespace CustomerWebsite.Controllers
{
    public class CartController : CoreController
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICartService cartService;
        private readonly IBillingAddressService billingAddressService;
        private readonly IShippingAddressService shippingAddressService;
        public CartController(IMemoryCache memoryCache, ICartService cartService, IBillingAddressService billingAddressService, IShippingAddressService shippingAddressService)
        {
            this.memoryCache = memoryCache;
            this.cartService = cartService;
            this.billingAddressService = billingAddressService;
            this.shippingAddressService = shippingAddressService;
        }

        #region Mapping logic
        [HttpPost]
        public IActionResult GetBasketData([FromBody] CartViewResponseDto cartDto)
        {
            memoryCache.Set(Auth0UserId, cartDto);
            var response = new { Message = "OK" };
            return Ok(response);
        }

        #endregion
        public async Task<IActionResult> Index()
        {
            ProductResponseDto[] emptyResponse = new ProductResponseDto[0];
            CartViewResponseDto cartData;
            bool result = memoryCache.TryGetValue(Auth0UserId, out cartData); 
            if (cartData.CartItems != null && result == true )
            {
                var cartIds = cartData.CartItems.Keys.ToList();
                return View(await cartService.GetSpecificProductsAsync(cartIds));
            }
            else return View(emptyResponse);
        }

        public async Task<IActionResult> Summary()
        {
            /* Variables */

            ProductResponseDto[] cartSummaryDto;
            BillingAddressResponseDto[] billingAddressSummaryDto;
            ShippingAddressResponseDto[] shippingAddressSummaryDto;
            BasketSummaryDto basketSummaryDto;
            string userId = Auth0UserId;

            /* Retrieve cart data */

            CartViewResponseDto cartData;
            bool result = memoryCache.TryGetValue(Auth0UserId, out cartData);
            if (cartData.CartItems != null && result == true)
            {
                var cartIds = cartData.CartItems.Keys.ToList();
                cartSummaryDto = await cartService.GetSpecificProductsAsync(cartIds);
            }
            else cartSummaryDto = new ProductResponseDto[0];

            /* Retrieve Billing address */
            try
            {
                billingAddressSummaryDto = await billingAddressService.GetBillingAddressAsync(userId);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Product");
            }

            /* Retrieve Shipping address */
            try
            {
                shippingAddressSummaryDto = await shippingAddressService.GetShippingAddressAsync(userId);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Product");
            }

            /* Map to a new Dto */
            basketSummaryDto = new BasketSummaryDto()
            {
                ProductResponseDtos = cartSummaryDto,
                BillingAddressResponseDtos = billingAddressSummaryDto,
                shippingAddressResponseDtos = shippingAddressSummaryDto
            };

            return View(basketSummaryDto);
        }
    }
}
