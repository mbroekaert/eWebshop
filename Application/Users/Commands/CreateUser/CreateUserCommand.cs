using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
        public string Auth0UserId { get; set; }
        public string Password { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                UserName = request.UserName,
                UserEmail = request.UserEmail,
                IsActive = request.IsActive,
                Auth0UserId = request.Auth0UserId,
                Password = "hidden"
            };

            _context.User.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.UserId;
        }
    }
}

