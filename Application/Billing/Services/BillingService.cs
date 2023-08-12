using Application.Common.Interfaces;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;

namespace Application.Billing.Services
{
    /* Services to call internal Payment API's */

    public class BillingService : IBillingService
    {
        private readonly HttpClient _httpClient;

        public BillingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TestConnection()
        {
            var httpResponse = await _httpClient.GetAsync("payment");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string>(responseAsString);
        }

        public async Task<CreateHostedCheckoutResponseDto> CreateHostedCheckout(CreateHostedCheckoutRequestDto request)
        {
            var content = JsonSerializer.Serialize(request);
            var httpResponse = await _httpClient.PostAsync("payment", new StringContent(content, Encoding.Default, "application/json"));
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CreateHostedCheckoutResponseDto>(responseAsString);

        }
    }
}
