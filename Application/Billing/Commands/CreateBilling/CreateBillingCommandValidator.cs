using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Billing.Commands.CreateBilling
{
    public class CreateBillingCommandValidator : AbstractValidator<CreateBillingCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateBillingCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.PaymentPayid)
                .NotEmpty().WithMessage("PAYID is required.");
            RuleFor(v => v.PaymentReference)
                .NotEmpty().WithMessage("Reference is required.");
            RuleFor(v => v.PaymentStatus)
                .NotEmpty().WithMessage("Status is required.");
        }
    }
}
