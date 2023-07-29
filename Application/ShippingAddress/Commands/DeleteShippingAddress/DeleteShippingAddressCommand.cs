using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.ShippingAddress.Commands.DeleteShippingAddress 
{ 
    public class DeleteShippingAddressCommand : IRequest
    {
        public int ShippingAddressId { get; set; }
    }

    public class DeleteShippingAddressCommandHandler : IRequestHandler<DeleteShippingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteShippingAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShippingAddress.FindAsync(request.ShippingAddressId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ShippingAddress), request.ShippingAddressId);
            }

            _context.ShippingAddress.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
