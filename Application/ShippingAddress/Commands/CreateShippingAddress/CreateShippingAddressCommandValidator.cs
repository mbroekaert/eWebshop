using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAddress.Commands.CreateShippingAddress
{
    public class CreateShippingAddressCommandValidator : AbstractValidator<CreateShippingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateShippingAddressCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.ShippingAddressStreetName)
                .NotEmpty().WithMessage("Street name is required.")
                .MaximumLength(50).WithMessage("Name of a street cannot exceed 50 characters");
            RuleFor(v => v.ShippingAddressStreetNumber)
              .NotEmpty().WithMessage("House number is required.");
            RuleFor(v => v.ShippingAddressCity)
              .NotEmpty().WithMessage("City is required.");
            RuleFor(v => v.ShippingAddressZip)
              .NotEmpty().WithMessage("Zip is required.");
            RuleFor(v => v.ShippingAddressCountry)
              .NotEmpty().WithMessage("Country is required.");
        }
       
    }
}
