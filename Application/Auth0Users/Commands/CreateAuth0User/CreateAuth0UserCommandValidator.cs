using FluentValidation;

namespace Application.Auth0Users.Commands.CreateAuth0User
{
    public class CreateAuth0UserCommandValidator : AbstractValidator<CreateAuth0UserCommand>
    {
        public CreateAuth0UserCommandValidator()
        {
            RuleFor(v => v.email)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("Email of a user cannot exceed 50 characters")
                .EmailAddress().WithMessage("Please provide a valid email address");
            RuleFor(v => v.password)
                .NotEmpty().WithMessage("Please provide a password")
                .NotNull().WithMessage("Please provide a password")
                .MinimumLength(5).WithMessage("Your password length must be at least 5.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }

    }
}
