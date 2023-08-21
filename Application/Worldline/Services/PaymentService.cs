using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using OnlinePayments.Sdk;
using OnlinePayments.Sdk.Domain;
using OnlinePayments.Sdk.Merchant;
using System.Text.Json;

namespace Application.Worldline.Services
{
    /* Services to call Worldline API's */
    public class PaymentService : IPaymentService
    {
        private IClient client;
        private IMerchantClient merchantClient;
        private string merchantId;
        private string apiKey;
        private string apiSecret;
        private string integrator;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            this._configuration = configuration;

            this.apiKey = _configuration["Worldline:ApiKey"];
            this.apiSecret = _configuration["Worldline:ApiSecret"];
            this.merchantId = _configuration["Worldline:MerchantId"];
            this._httpClient = httpClient;
            this.integrator = "MTB-PSR11069";
            Uri apiEndpoint = new Uri("https://payment.preprod.direct.worldline-solutions.com");		
            client = Factory.CreateClient(apiKey, apiSecret, apiEndpoint, integrator);
            merchantClient = client.WithNewMerchant(merchantId);
        }

        public async Task<string> TestConnection()
        {
            var httpResponse = await merchantClient.Services.TestConnection();
            return JsonSerializer.Serialize(httpResponse);
        }

        public async Task<CreateHostedCheckoutResponse> CreateHostedCheckout(CreateHostedCheckoutRequest request)
        {
            var httpResponse = await merchantClient.HostedCheckout.CreateHostedCheckout(request);
            return httpResponse;
        }
        public async Task<RefundResponse> CreateRefund (string PaymentId, RefundRequest request)
        {
            var httpResponse = await merchantClient.Payments.RefundPayment(PaymentId, request);
            return httpResponse;
        }
    }
}
