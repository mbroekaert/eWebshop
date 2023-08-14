using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Billing.Commands.CreateBilling
{
    public class CreateBillingCommand : IRequest<int>
    {
        public string PaymentPayid { get; set; }
        public string PaymentReference { get; set; }
        public int PaymentStatus { get; set; }
        public int OrderId { get; set; }
    }

    public class CreateBillingCommandHandler : IRequestHandler<CreateBillingCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateBillingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBillingCommand request, CancellationToken cancellationToken)
        {
            var entity = new Payment
            {
                PaymentPayid = request.PaymentPayid,
                PaymentReference = request.PaymentReference,
                PaymentStatus = request.PaymentStatus,
                OrderId = request.OrderId,

            };

            _context.Payment.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.PaymentId;
        }
    }
}
