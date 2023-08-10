using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlinePayments.Sdk;
using OnlinePayments.Sdk.Domain;
using OnlinePayments.Sdk.Merchant;
using Shared.Contracts.Response;
using System.Net.Http;
using System.Text;
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

        public PaymentService(HttpClient httpClient)
        {
            //this.apiKey = builder.Configuration["Worldline:ApiKey"];
            //this.apiSecret = builder.Configuration["Worldline:ApiSecret"];
            //this.merchantId = builder.Configuration["Worldline:MerchantId"];
            this._httpClient = httpClient;
            this.merchantId = "MTBTfe";
            this.apiKey = "C0C57F9A9F962A0E10CB";
            this.apiSecret = "SHwQAj56Q+ktRWdCRR+bSkfiL9MztJYKY6AyxWe60nNOHLHtcGwnpHS8FeW6YHykCWzg7D6N+ZA6U0mKDbRWrA==";
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
    }
}
