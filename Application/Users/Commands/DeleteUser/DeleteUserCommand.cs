using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.DeleteUser
{

    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            _context.Users.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
