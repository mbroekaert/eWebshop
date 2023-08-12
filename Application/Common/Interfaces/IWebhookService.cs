using Microsoft.AspNetCore.Http;
using OnlinePayments.Sdk;

namespace Application.Common.Interfaces
{
    public interface IWebhookService
    {
        Task<WebhooksEvent> HandleWebhook(HttpRequest httpRequest);
    }
}
