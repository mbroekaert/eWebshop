using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto[]> GetUsersAsync();
        Task<(bool success, string content)> CreateUserAsync(User user);
        Task<UserResponseDto> EditUserAsync(int id);
        Task<(bool success, string content)> UpdateUserAsync(User user);
        Task<UserResponseDto> GetUserToDeleteAsync(int id);
        Task<(bool success, string content)> DeleteUserAsync(User user);
    }
}
