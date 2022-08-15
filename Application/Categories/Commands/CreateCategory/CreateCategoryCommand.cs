using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name,
                DisplayOrder = request.DisplayOrder,
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}

