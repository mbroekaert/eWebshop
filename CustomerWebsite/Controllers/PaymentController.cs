using Application.Billing.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class PaymentController : CoreController
    {
        private readonly BillingService _billingService;

        public PaymentController(BillingService billingService)
        {
            _billingService = billingService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
