using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface ITodoItemsService
    {
        Task<ToDoItemResponseDto[]> GetTodosAsync();
    }
}
