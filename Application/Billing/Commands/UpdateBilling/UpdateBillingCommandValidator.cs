using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Billing.Commands.UpdateBilling
{
    public class UpdateBillingCommandValidator : AbstractValidator<UpdateBillingCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBillingCommandValidator(IApplicationDbContext context)
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
