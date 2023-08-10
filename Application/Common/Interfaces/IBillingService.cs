using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;

namespace Application.Common.Interfaces
{
    public interface IBillingService
    {
        /* Services to call internal Payment API's */
        Task<string> TestConnection();
        public Task<CreateHostedCheckoutResponse> CreateHostedCheckout(CreateHostedCheckoutRequestDto request);
    }
}
