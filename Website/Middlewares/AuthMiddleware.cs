using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using System.Text;

namespace Website.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, HttpClient httpClient)
        {
            if (context.User.Identity.IsAuthenticated) // to do : vérifier si le token est expiré pour relancer la requête.
            {
                string accessToken = await context.GetTokenAsync("access_token"); // used to call API's + access rights - authorization
                DateTime accessTokenExpiresAt = DateTime.Parse(await context.GetTokenAsync("expires_at"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                string idToken = await context.GetTokenAsync("id_token"); // Used to confirm the user is authenticated through the app - authentication
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
            }
            await _next(context);
        }
    }
}
