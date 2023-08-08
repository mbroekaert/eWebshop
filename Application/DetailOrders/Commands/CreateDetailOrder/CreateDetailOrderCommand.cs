using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.DetailOrders.Commands.CreateDetailOrder
{
    public class CreateDetailOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateDetailOrderCommandHandler : IRequestHandler<CreateDetailOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDetailOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDetailOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new DetailOrder
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            _context.DetailOrder.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.DetailOrderId;
        }
    }
}
