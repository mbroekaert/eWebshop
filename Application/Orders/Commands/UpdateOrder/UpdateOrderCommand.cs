using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        public string OrderReference { get; set; }
        public double OrderAmount { get; set; }
        public DateTime Orderdate { get; set; }
        public string Status { get; set; }
        public string CustomerAuth0UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; }

    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Order.FindAsync(request.OrderId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }
            entity.OrderId = request.OrderId;
            entity.OrderReference = request.OrderReference;
            entity.OrderAmount = request.OrderAmount;
            entity.Status = request.Status;
            entity.CustomerAuth0UserId = request.CustomerAuth0UserId;
            entity.OrderDate = request.Orderdate;
            entity.ShippingAddressId = request.ShippingAddressId;
            entity.BillingAddressId = request.BillingAddressId;

            await _context.SaveChangesAsync(cancellationToken);
            return entity.OrderId;
        }
    }
}
