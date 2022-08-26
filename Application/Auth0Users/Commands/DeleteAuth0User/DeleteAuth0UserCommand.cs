using Application.Auth0Users.Services;
using MediatR;
using System.Net.Http.Headers;

namespace Application.Auth0Users.Commands.DeleteAuth0User
{
    public class DeleteAuth0UserCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class DeleteAuth0UserCommandHandler : IRequestHandler<DeleteAuth0UserCommand, bool>
    {
        private readonly HttpClient _httpClient;
        private readonly GetAuth0ManagementTokenService service;


        public DeleteAuth0UserCommandHandler(HttpClient client, GetAuth0ManagementTokenService service)
        {
            _httpClient = client;
            this.service = service;
        }

        public async Task<bool> Handle(DeleteAuth0UserCommand request, CancellationToken cancellationToken)
        {
            /* Retrieve access token */
            var token = await service.GetManagementApiAccessTokenAsync();

            /* Retrieve user to delete */
            var userId = request.Id;

            /* Adding token on header */
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            /* Send request */

            var httpResponse = await _httpClient.DeleteAsync($"https://mathieubroekaert.eu.auth0.com/api/v2/users/{userId}");
            var response = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
