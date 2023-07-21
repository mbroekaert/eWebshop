using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string CategoryName { get; set; }
        public int CategoryDisplayOrder { get; set; }
        public string CategoryDescription { get; set; }

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
                CategoryName = request.CategoryName,
                CategoryDisplayOrder = request.CategoryDisplayOrder,
                CategoryDescription = request.CategoryDescription
            };

            _context.Category.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.CategoryId;
        }
    }
}

