using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Tokens.Commands.DeleteToken
{
    public class DeleteTokenCommand : IRequest
    {
        public string TokenId { get; set; }
    }

    public class DeleteTokenCommandHandler : IRequestHandler<DeleteTokenCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTokenCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTokenCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Token.FindAsync(request.TokenId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Token), request.TokenId);
            }

            _context.Token.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
