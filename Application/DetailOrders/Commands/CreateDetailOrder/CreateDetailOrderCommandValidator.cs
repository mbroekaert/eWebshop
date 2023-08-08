using Application.Common.Interfaces;
using Application.DetailOrders.Commands.CreateDetailOrder;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateDetailOrderCommandValidator : AbstractValidator<CreateDetailOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateDetailOrderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.OrderId)
                .NotEmpty().WithMessage("Order Id is required.");
            RuleFor(v => v.ProductId)
                .NotEmpty().WithMessage("Product Id is required.");
        }


    }
}
