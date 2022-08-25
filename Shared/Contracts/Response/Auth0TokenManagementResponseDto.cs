﻿using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public  class Auth0TokenManagementResponseDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
       
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; } = "Test123!";
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

    }
}
