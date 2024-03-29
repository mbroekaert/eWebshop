﻿using Application.Auth0Users.Services;
using MediatR;
using Shared.Contracts.Request;
using Shared.Contracts.Response;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Auth0Users.Commands.CreateAuth0User
{
    public class CreateAuth0UserCommand : IRequest<(bool, string)>
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

    }

    public class CreateAuth0UserCommandHandler : IRequestHandler<CreateAuth0UserCommand, (bool, string)>
    {
        private readonly HttpClient _httpClient;
        private readonly GetAuth0ManagementTokenService service;


        public CreateAuth0UserCommandHandler(HttpClient client, GetAuth0ManagementTokenService service)
        {
            _httpClient = client;
            this.service = service;
        }

        public async Task<(bool, string)> Handle(CreateAuth0UserCommand request, CancellationToken cancellationToken)
        {
            /* Retrieve access token */
            var token = await service.GetManagementApiAccessTokenAsync();

            /* Post request to create user */
            var entity = new Auth0UserRequestDto
            {
                UserEmail = request.UserEmail,
                Password = request.Password,
            };

            var content = JsonSerializer.Serialize(entity);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await _httpClient.PostAsync(("https://mathieubroekaert.eu.auth0.com/api/v2/users"), new StringContent(content, Encoding.Default, "application/json"));
            var response = await httpResponse.Content.ReadAsStringAsync();

            /* Assign default role to user */

            if (httpResponse.IsSuccessStatusCode)
            {
                // Retrieve UserId

                Auth0UserCreationResponseDto user = JsonSerializer.Deserialize<Auth0UserCreationResponseDto>(response);
                string userId = user.UserId;

                //Assign roles

                var roles = new Auth0InitialRoleRequestDto();
                var defaultRole = JsonSerializer.Serialize(roles);
                var rolesHttpReponse = await _httpClient.PostAsync($"https://mathieubroekaert.eu.auth0.com/api/v2/users/{userId}/roles", new StringContent(defaultRole, Encoding.Default, "application/json"));
                var resultoto = await rolesHttpReponse.Content.ReadAsStringAsync();
                if (rolesHttpReponse.IsSuccessStatusCode)
                {
                    return (true, userId);
                }
                return (false, userId);
            }
            return (false, "");

        }

    }
}
