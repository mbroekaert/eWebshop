using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.BillingAddress.Commands.DeleteBillingAddress
{
    public class DeleteBillingAddressCommand : IRequest
    {
        public int BillingAddressId { get; set; }
    }

    public class DeleteBillingAddressCommandHandler : IRequestHandler<DeleteBillingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBillingAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBillingAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BillingAddress.FindAsync(request.BillingAddressId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(BillingAddress), request.BillingAddressId);
            }

            _context.BillingAddress.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
