using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Billing.Commands.UpdateBilling
{
    public class UpdateBillingCommand : IRequest<Unit>
    {
        public int PaymentId { get; set; }
        public string PaymentPayid { get; set; }
        public string PaymentReference { get; set; }
        public int PaymentStatus { get; set; }
        public int OrderId { get; set; }
    }

    public class UpdateBillingCommandHandler : IRequestHandler<UpdateBillingCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBillingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBillingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Payment.FindAsync(request.PaymentId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Payment), request.PaymentPayid);
            }
            entity.PaymentId = request.PaymentId;
            entity.PaymentPayid = request.PaymentPayid;
            entity.PaymentReference = request.PaymentReference;
            entity.PaymentStatus = request.PaymentStatus;
            entity.OrderId = request.OrderId;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
