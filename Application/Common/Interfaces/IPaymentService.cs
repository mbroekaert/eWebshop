using OnlinePayments.Sdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    /* Services to call Worldline API's */
    public interface IPaymentService
    {
        public Task<string> TestConnection();
        public Task<CreateHostedCheckoutResponse> CreateHostedCheckout(CreateHostedCheckoutRequest request);
    }
}
