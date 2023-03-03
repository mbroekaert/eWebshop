using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; }

    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Category.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }
            entity.CategoryName = request.Name;
            entity.CategoryDisplayOrder = request.DisplayOrder;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
