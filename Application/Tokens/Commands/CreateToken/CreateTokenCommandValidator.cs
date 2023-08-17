using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tokens.Commands.CreateToken
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTokenCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.TokenId)
                .NotEmpty().WithMessage("Token is required.")
                .MustAsync(BeUniqueName).WithMessage("The specified token already exists.");
        }


        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Token
                .AllAsync(l => l.TokenId != name);
        }
    }
}
