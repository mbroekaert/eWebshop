using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.DetailOrders.Commands.DeleteDetailsOrder
{
    public  class DeleteDetailOrdersCommand : IRequest
    {
        public int OrderId { get; set; }
    }

    public class DeleteDetailOrdersCommandHandler : IRequestHandler<DeleteDetailOrdersCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDetailOrdersCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDetailOrdersCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.DetailOrder.FindAsync(request.OrderId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.OrderId);
            }

            _context.DetailOrder.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
