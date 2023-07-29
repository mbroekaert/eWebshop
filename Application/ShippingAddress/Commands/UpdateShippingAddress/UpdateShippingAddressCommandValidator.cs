using Application.ShippingAddress.Commands.UpdateShippingAddress;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.ShippingAddress.Commands.UpdateShippingAddress
{
    public  class UpdateShippingAddressCommandValidator : AbstractValidator<UpdateShippingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateShippingAddressCommandValidator(IApplicationDbContext context)
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
