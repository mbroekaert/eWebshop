using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.DeleteOrder
{
    public  class DeleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Order.FindAsync(request.OrderId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.OrderId);
            }

            _context.Order.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
