using Application.Billing.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlinePayments.Sdk;
using OnlinePayments.Sdk.Domain;
using OnlinePayments.Sdk.Webhooks;
using Shared.Contracts.Request;
using System.Text;

namespace CustomerWebsite.Controllers
{
    public class PaymentController : CoreController
    {
        private readonly IBillingService _billingService;
        private readonly IWebhookService _webhookService;
     

        public PaymentController(IBillingService billingService, IWebhookService webhookService)
        {
            _billingService = billingService;
            _webhookService = webhookService;
        }

        public async Task<IActionResult> OrderConfirmation([FromQuery]string RETURNMAC, int HostedCheckoutId)
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Webhook ()
        {
            var result = await _webhookService.HandleWebhook(Request);
            return View();
        }
    }
}
