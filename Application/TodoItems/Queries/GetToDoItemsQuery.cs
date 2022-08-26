using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Contracts.Response;
using System.Text.Json;

namespace Application.TodoItems.Queries
{
    public class GetToDoItemsQuery : IRequest<IEnumerable<ToDoItemResponseDto>>
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

    }

    public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, IEnumerable<ToDoItemResponseDto>>
    {
        private readonly IMapper _mapper;
        private HttpClient _httpClient;

        public GetToDoItemsQueryHandler(IMapper mapper, HttpClient client)
        {
            _mapper = mapper;
            _httpClient = client;
        }

        public async Task<IEnumerable<ToDoItemResponseDto>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
        {
            var httpResponse = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<ToDoItemResponseDto[]>(responseAsString);
            return deserializedResponse;


            //var categories = await _context.Categories
            //            .OrderBy(t => t.DisplayOrder)
            //            .ToListAsync(cancellationToken);
            //return categories.Select(c => _mapper.Map<CategoryResponseDto>(c)).ToList();
        }
    }
}
