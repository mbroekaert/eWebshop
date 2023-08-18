using Shared.Contracts.Request;
using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(bool success, string content)> CreateTokenAsync(TokenRequestDto token);
        Task<TokenResponseDto[]> GetTokensAsync(string userId);
        Task<(bool success, string content)> DeleteTokenAsync(string tokenId);
    }
}
