using Application.Billing.Services;
using Microsoft.AspNetCore.Mvc;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;

namespace CustomerWebsite.Controllers
{
    public class PaymentController : CoreController
    {
        private readonly BillingService _billingService;

        public PaymentController(BillingService billingService)
        {
            _billingService = billingService;
        }

        //public async Task<IActionResult> CreateHostedCheckout(CreateHostedCheckoutRequestDto request)
        //{
        //    var response = await _billingService.CreateHostedCheckout(request);
        //    return Redirect(response.RedirectUrl);
        //}
        public async Task<IActionResult> OrderConfirmation()
        {
            return View();
        }
    }
}
