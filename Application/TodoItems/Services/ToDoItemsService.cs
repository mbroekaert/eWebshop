using Application.Common.Interfaces;
using Shared.Contracts.Response;
using System.Text.Json;

namespace Application.TodoItems.Services
{
    public class ToDoItemsService : ITodoItemsService
    {
        private readonly HttpClient _httpClient;

        public ToDoItemsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ToDoItemResponseDto[]> GetTodosAsync()
        {
            var httpResponse = await _httpClient.GetAsync("toDoItems");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ToDoItemResponseDto[]>(responseAsString);
        }
    }
}