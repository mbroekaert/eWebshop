using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.OrderReference)
                .NotEmpty().WithMessage("Reference is required.")
                .MustAsync(BeUniqueOrderReference).WithMessage("The specified reference already exists.");
            RuleFor(v => v.OrderAmount)
                .NotEmpty().WithMessage("Please specify a price")
                .ExclusiveBetween(0.0, double.MaxValue).WithMessage("Must be >= 0");
            RuleFor(v => v.OrderReference)
                .NotEmpty().WithMessage("Reference is required.");
            RuleFor(v=> v.Status)
                .Equal("Created").WithMessage("Status must be 'Created'");

        }

        public async Task<bool> BeUniqueOrderReference(string orderReference, CancellationToken cancellationToken)
        {
            return await _context.Order
                .AllAsync(l => l.OrderReference != orderReference);
        }

    }
}
