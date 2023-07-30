using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace CustomerWebsite.Controllers
{
    public class CartController : CoreController
    {
        #region Mapping logic
        private CartViewResponseDto GetCartData()
        {
            var cartItems = new Dictionary<int, int>();
            return new CartViewResponseDto
            {
                CartItems = cartItems,
            };
        }

        #endregion
        [HttpPost]
        public IActionResult Index()
        {
            var cartData = GetCartData();
            return Json(cartData);
        }
    }
}
