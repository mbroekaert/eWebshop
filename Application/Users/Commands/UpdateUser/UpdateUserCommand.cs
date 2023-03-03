using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }

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
            var entity = await _context.User.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }
            entity.UserName = request.Name;
            entity.UserEmail = request.Email;
            entity.IsActive = request.IsActive;
            entity.Auth0UserId = request.UserId;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
