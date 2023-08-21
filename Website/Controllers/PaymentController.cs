using Application.Billing.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;

namespace Website.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IBillingService _billingService;
        private readonly IOrderService _orderService;

        public PaymentController(IBillingService billingService, IOrderService orderService)
        {
            this._billingService = billingService;
            this._orderService = orderService;
        }
        public async Task<ActionResult> TestConnection(TestConnection testResult)
        {
            return View(await _billingService.TestConnection());
        }

        [Route("[controller]/[action]/{orderId}")]
        [HttpGet]
        public async Task<IActionResult> RefundTransaction(int orderId)
        {
            // Retrieve Payment 
            var payment = await _billingService.GetPaymentByOrderId(orderId);
            // Retrieve order
            var orderList = await _orderService.GetOrderById(orderId);
            var order = orderList[0];
            // Create Dto
            RefundRequestDto refundRequest = new RefundRequestDto
            {
                OrderAmount = order.OrderAmount,
                PaymentPayid = payment.PaymentPayid,
                OrderReference = order.OrderReference
            };

            return View(refundRequest);
        }
        public async Task<IActionResult> ProcessRefund (RefundRequestDto refundRequest)
        {
            var result = await _billingService.CreateRefund(refundRequest);
            return RedirectToAction("Index", "Home");
        }
    }
}
