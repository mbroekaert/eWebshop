using Application.Common.Interfaces;
using Domain.Entities;
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

        public async Task<(bool result, string content)> CreatePayment(PaymentRequestDto request)
        {
            var content = JsonSerializer.Serialize(request);
            var httpResponse = await _httpClient.PostAsync("billing", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Payment created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<bool> CheckPaymentExistence(string PaymentPayid)
        {
            var httpResponse = await _httpClient.GetAsync($"billing/{PaymentPayid}");
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                if (responseAsString.Length > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
            
        }

        public async Task<PaymentResponseDto> GetPaymentByPaymentPayid(string PaymentPayid)
        {
            var httpResponse = await _httpClient.GetAsync($"billing/{PaymentPayid}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<PaymentResponseDto>(responseAsString);
            return deserializedResponse;
        }

        public async Task<(bool result, string content)> UpdatePaymentAsync(PaymentRequestDto request)
        {
            var content = JsonSerializer.Serialize(request);
            var httpResponse = await _httpClient.PutAsync($"billing/{request.PaymentId}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Payment updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<PaymentResponseDto> GetPaymentByOrderId(int orderId)
        {
            var httpResponse = await _httpClient.GetAsync($"billing/order/{orderId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<PaymentResponseDto>(responseAsString);
            return deserializedResponse;
        }

        public async Task<RefundResponse> CreateRefund(RefundRequestDto request)
        {
            var content = JsonSerializer.Serialize(request);
            var httpResponse = await _httpClient.PostAsync("payment/refund", new StringContent(content, Encoding.Default, "application/json"));
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<RefundResponse>(responseAsString);
            return JsonSerializer.Deserialize<RefundResponse>(responseAsString);
        }
    }
}
