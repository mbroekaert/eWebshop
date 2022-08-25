using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAuth0UserService
    {
       Task<(bool success, string content)> CreateAuth0UserAsync(User user);
    }
}
