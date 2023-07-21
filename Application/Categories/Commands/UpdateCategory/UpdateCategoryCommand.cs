using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryDisplayOrder { get; set; }
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
            var entity = await _context.Category.FindAsync(request.CategoryId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }
            entity.CategoryName = request.CategoryName;
            entity.CategoryDisplayOrder = request.CategoryDisplayOrder;
            entity.CategoryDescription = request.CategoryDescription;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
