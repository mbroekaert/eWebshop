using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandValidator (IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryName)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(50).WithMessage("Name of a category cannot exceed 50 characters");
            RuleFor(v => v.CategoryDescription)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(v => v.CategoryDisplayOrder)
                .NotEmpty().WithMessage("Please specify a display order")
                .ExclusiveBetween(0, 101).WithMessage("Must be between 1 and 100");

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
