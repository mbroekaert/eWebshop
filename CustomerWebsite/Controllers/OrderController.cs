using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class OrderController : CoreController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(List<int> productId, List<int> quantity, int billingAddressId, int shippingAddressId)
        {
            string CustomerId = Auth0UserId;
            Order newOrder = new Order
            {
                OrderAmount = 0,
                CustomerAuth0UserId = CustomerId,
                ShippingAddressId = shippingAddressId,
                BillingAddressId = billingAddressId
            };
            var result = await _orderService.CreateOrder(newOrder, productId, quantity);
            if (result.success)
            {
                return View(newOrder);
            }
            return View(newOrder);
        }
    }
}
