using Application.Billing.Services;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Request;
using Shared.Contracts.Response;
using System.Text.Json;

namespace CustomerWebsite.Controllers
{
    public class OrderController : CoreController
    {
        private readonly IOrderService _orderService;
        private readonly IShippingAddressService _shippingAddressService;
        private readonly IBillingAddressService _billingAddressService;
        private readonly IBillingService _billingService;
        public OrderController(IOrderService orderService, IShippingAddressService shippingAddressService, IBillingAddressService billingAddressService, IBillingService billingService)
        {
            _orderService = orderService;
            _shippingAddressService = shippingAddressService;
            _billingAddressService = billingAddressService;
            _billingService = billingService;
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
            int OrderId = result.OrderId;
            if (result.success)
            {
                /* Retrieve needed parameters for payment */

                var order = (await _orderService.GetOrderById(OrderId))[0];
                var shippingAddress = await _shippingAddressService.GetShippingAddressById(order.ShippingAddressId);
                var billingAddress = await _billingAddressService.GetBillingAddressById (order.BillingAddressId);

                CreateHostedCheckoutRequestDto request = new CreateHostedCheckoutRequestDto
                {
                    amount = order.OrderAmount,
                    currency = "EUR",
                    orderReference = order.OrderReference,
                    returnUrl = "https://localhost:7276/payment/orderconfirmation",
                    billingAddressRequestDto = new BillingAddressRequestDto
                    {
                        billingAddressId = billingAddress.BillingAddressId,
                        billingAddressCity = billingAddress.BillingAddressCity,
                        billingAddressCountry = billingAddress.BillingAddressCountry,
                        billingAddressZip = billingAddress.BillingAddressZip,
                        billingAddressStreetName = billingAddress.BillingAddressStreetName,
                        billingAddressStreetNumber = billingAddress.BillingAddressStreetNumber
                    },
                    shippingAddressRequestDto = new ShippingAddressRequestDto
                    {
                        ShippingAddressId = shippingAddress.ShippingAddressId,
                        ShippingAddressCity = shippingAddress.ShippingAddressCity,
                        ShippingAddressCountry = shippingAddress.ShippingAddressCountry,
                        ShippingAddressZip = shippingAddress.ShippingAddressZip,
                        ShippingAddressStreetName = shippingAddress.ShippingAddressStreetName,
                        ShippingAddressStreetNumber = shippingAddress.ShippingAddressStreetNumber
                    }
                };

                TempData["HostedCheckout"] = JsonSerializer.Serialize(request);

                /* Calling controller to create Hosted Checkout */

                return RedirectToAction("CreateHostedCheckout");
            }
            return RedirectToAction("ViewOrders");
        }

        public async Task<IActionResult> CreateHostedCheckout()
        {
            var serializedRequest = TempData["HostedCheckout"] as string;
            var request = JsonSerializer.Deserialize<CreateHostedCheckoutRequestDto>(serializedRequest);
            var response = await _billingService.CreateHostedCheckout(request);
            return Redirect(response.redirectUrl);
        }
        public async Task<ActionResult> ViewOrders ()
        {
            string userId = Auth0UserId;
            return View (await _orderService.GetCustomerOrders(userId));
        }
    }
}
