using Application.Common.Interfaces;
using Application.Orders.Commands.UpdateOrder;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.OrderReference)
                .NotEmpty().WithMessage("Reference is required.");
                //.MustAsync(BeUniqueOrderReference).WithMessage("The specified reference already exists.");
            RuleFor(v => v.OrderAmount)
                .NotEmpty().WithMessage("Please specify a price")
                .ExclusiveBetween(0.0, double.MaxValue).WithMessage("Must be >= 0");
            RuleFor(v => v.OrderReference)
                .NotEmpty().WithMessage("Reference is required.");

        }

        //public async Task<bool> BeUniqueOrderReference(string orderReference, CancellationToken cancellationToken)
        //{
        //    return await _context.Order
        //        .AllAsync(l => l.OrderReference != orderReference);
        //}

    }
}
