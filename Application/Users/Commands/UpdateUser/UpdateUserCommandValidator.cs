﻿using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name of a user cannot exceed 50 characters");
            RuleFor(v => v.IsActive)
                .NotNull().WithMessage("Please specify if the user is active");
            RuleFor(v => v.UserEmail)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("Email of a user cannot exceed 50 characters")
                .EmailAddress().WithMessage("Please provide a valid email address");
                /* Find a way to avoid duplicates
                .MustAsync(BeUniqueEmailAddress).WithMessage("This email is already used"); */
        }

        public async Task<bool> BeUniqueEmailAddress(string email, CancellationToken cancellationToken)
        {
            return await _context.User
                .AllAsync(l => l.UserEmail != email);
        }
    }
}
