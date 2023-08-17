using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using OnlinePayments.Sdk;
using OnlinePayments.Sdk.Webhooks;
using System.Text;

namespace Application.Worldline.Services
{
    public class WebhookService : IWebhookService
    {
        private string keyId;
        private string secretKey;

        public WebhookService()
        {
            keyId = "666af1f1e4ee4c2ea4bb39172f6102";
            secretKey = "4c99c280-f82b-4e8a-a3f2-e7d84ac90774";
        }

        public async Task<WebhooksEvent> HandleWebhook(HttpRequest httpRequest)
        {
            InMemorySecretKeyStore.Instance.Clear();
            InMemorySecretKeyStore.Instance.StoreSecretKey(keyId, secretKey);
            WebhooksHelper helper = Webhooks.CreateHelper(InMemorySecretKeyStore.Instance);

            /* Retrieve body */
            string bodyOfRequest;
            using (StreamReader reader = new StreamReader(httpRequest.Body, Encoding.UTF8))
            {
                bodyOfRequest = await reader.ReadToEndAsync();
            }

            /* Retrieve headers */

            var webhookHeaders = httpRequest.Headers;
            var requestHeaders = new List<RequestHeader>();
            foreach (var (name, value) in webhookHeaders)
            {
                requestHeaders.Add(new RequestHeader(name, value));
            }
            WebhooksEvent webhooksEvent = helper.Unmarshal(bodyOfRequest, requestHeaders);
            return webhooksEvent;
        }
    }
}
