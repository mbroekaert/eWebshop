using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.BillingAddress.Commands.CreateBillingAddress
{
    public class CreateBillingAddressCommandValidator : AbstractValidator<CreateBillingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateBillingAddressCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.BillingAddressStreetName)
                .NotEmpty().WithMessage("Street name is required.")
                .MaximumLength(50).WithMessage("Name of a street cannot exceed 50 characters");
            RuleFor(v => v.BillingAddressStreetNumber)
              .NotEmpty().WithMessage("House number is required.");
            RuleFor(v => v.BillingAddressCity)
              .NotEmpty().WithMessage("City is required.");
            RuleFor(v => v.BillingAddressZip)
              .NotEmpty().WithMessage("Zip is required.");
            RuleFor(v => v.BillingAddressCountry)
              .NotEmpty().WithMessage("Country is required.");
        }
       
    }
}
