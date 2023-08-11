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
    }
}
