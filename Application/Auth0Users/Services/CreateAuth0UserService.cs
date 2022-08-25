using Microsoft.Extensions.Configuration;

namespace Application.Auth0Users.Services
{
    public class CreateAuth0UserService 
    {
        private readonly string domain;
        private readonly string clientId;
        private readonly string clientSecret;

        public CreateAuth0UserService(IConfiguration config)
        {
            this.domain = config["Auth0:Domain"];
            this.clientId = config["Auth0:Management:ClientId"];
            this.clientSecret = config["Auth0:Management:ClientSecret"];
        }



    }
}
