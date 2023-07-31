using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.Contracts.Response;

namespace CustomerWebsite.Controllers
{
    public class CartController : CoreController
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICartService cartService;
        public CartController(IMemoryCache memoryCache, ICartService cartService)
        {
            this.memoryCache = memoryCache;
            this.cartService = cartService;
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
    }
}
