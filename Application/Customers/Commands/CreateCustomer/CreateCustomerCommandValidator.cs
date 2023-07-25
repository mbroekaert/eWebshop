using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("Name is required.");
            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Name is required.");
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("Email of a user cannot exceed 50 characters")
                .EmailAddress().WithMessage("Please provide a valid email address")
                .MustAsync(BeUniqueEmailAddress).WithMessage("This email is already used");
            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Phone number is required");
        }

        public async Task<bool> BeUniqueEmailAddress(string email, CancellationToken cancellationToken)
        {
            return await _context.Customer
                .AllAsync(l => l.CustomerEmail != email);
        }
    }
}
