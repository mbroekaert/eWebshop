using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;
using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface IBillingService
    {
        /* Services to call internal Payment API's */
        Task<string> TestConnection();
        public Task<CreateHostedCheckoutResponseDto> CreateHostedCheckout(CreateHostedCheckoutRequestDto request);
        public Task<(bool result, string content)> CreatePayment(PaymentRequestDto request);
        public Task<bool> CheckPaymentExistence(string PaymentPayid);
        public Task<PaymentResponseDto> GetPaymentByPaymentPayid (string PaymentPayid);
        public Task<(bool result, string content)> UpdatePaymentAsync(PaymentRequestDto request);
        public Task<PaymentResponseDto> GetPaymentByOrderId(int orderId);
        public Task<RefundResponse> CreateRefund (RefundRequestDto request);
    }
}
