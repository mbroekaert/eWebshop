using Application.Billing.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlinePayments.Sdk.Domain;

namespace Website.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IBillingService _billingService;

        public PaymentController(IBillingService billingService)
        {
            this._billingService = billingService;
        }
        public async Task<ActionResult> TestConnection(TestConnection testResult)
        {
            return View(await _billingService.TestConnection());
        }
    }
}
