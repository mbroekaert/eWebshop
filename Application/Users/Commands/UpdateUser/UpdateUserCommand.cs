using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
        public string Auth0UserId { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.User.FindAsync(request.UserId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.UserId);
            }
            entity.UserName = request.UserName;
            entity.UserEmail = request.UserEmail;
            entity.IsActive = request.IsActive;
            entity.Auth0UserId = request.Auth0UserId;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
