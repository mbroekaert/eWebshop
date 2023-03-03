using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name of a category cannot exceed 50 characters")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
            RuleFor(v => v.DisplayOrder)
                .NotEmpty().WithMessage("Please specify a display order")
                .MustAsync(BeUniqueDisplayOrder).WithMessage("This number is already used")
                .ExclusiveBetween(1,100).WithMessage("Must be between 1 and 100");
        }

        public async Task<bool> BeUniqueDisplayOrder(int displayOrder, CancellationToken cancellationToken)
        {
            return await _context.Category
                .AllAsync(l => l.CategoryDisplayOrder != displayOrder);
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Category
                .AllAsync(l => l.CategoryName != name);
        }
    }
}
