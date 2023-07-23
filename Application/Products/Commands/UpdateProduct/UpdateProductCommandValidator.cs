using Application.Common.Interfaces;
using Application.Products.Commands.CreateProduct;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public  class UpdateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.ProductName)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(50).WithMessage("Name of a product cannot exceed 50 characters")
               .MustAsync(BeUniqueProductName).WithMessage("The specified name already exists.");
            RuleFor(v => v.ProductReference)
                .NotEmpty().WithMessage("Reference is required.")
                .MaximumLength(50).WithMessage("Reference of a product cannot exceed 50 characters")
                .MustAsync(BeUniqueProductReference).WithMessage("The specified reference already exists.");
            RuleFor(v => v.ProductPrice)
                .NotEmpty().WithMessage("Please specify a price")
                .ExclusiveBetween(0.0, double.MaxValue).WithMessage("Must be >= 0");
            RuleFor(v => v.ProductQuantity)
                .NotEmpty().WithMessage("Please specify a stock")
                .ExclusiveBetween(0, 100000).WithMessage("Must be >= 0 and cannot exceed 100000");

        }
        public async Task<bool> BeUniqueProductName(string productName, CancellationToken cancellationToken)
        {
            return await _context.Product
                .AllAsync(l => l.ProductName != productName);
        }

        public async Task<bool> BeUniqueProductReference(string productReference, CancellationToken cancellationToken)
        {
            return await _context.Product
                .AllAsync(l => l.ProductName != productReference);
        }
    }
}
