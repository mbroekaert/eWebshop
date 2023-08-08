using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public string OrderReference { get; set; }
        public double OrderAmount { get; set; }
        public DateTime Orderdate { get; set; }
        public string Status { get; set; }
        public string CustomerAuth0UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; }

    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new Order
            {
                OrderReference = request.OrderReference,
                OrderAmount = request.OrderAmount,
                OrderDate = request.Orderdate,
                Status = request.Status,
                CustomerAuth0UserId = request.CustomerAuth0UserId,
                ShippingAddressId = request.ShippingAddressId,
                BillingAddressId = request.BillingAddressId

            };

            _context.Order.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.OrderId;
        }
    }
}
