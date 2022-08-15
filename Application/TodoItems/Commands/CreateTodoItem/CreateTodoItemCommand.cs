using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Title = request.Title,
                Note = "DefaultNote",
                Done = false
            };

            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
