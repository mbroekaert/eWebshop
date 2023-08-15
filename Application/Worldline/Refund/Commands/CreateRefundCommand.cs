using Application.Common.Interfaces;
using MediatR;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;

namespace Application.Worldline.Refund.Commands
{
    public class CreateRefundCommand : IRequest<RefundResponse>
    {
        public double orderAmount { get; set; }
        public string orderReference { get; set; }
        public string PaymentId { get; set; }
        
    }

    public class CreateRefundCommandHandler : IRequestHandler<CreateRefundCommand, RefundResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPaymentService _paymentService;

        public CreateRefundCommandHandler(IApplicationDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;   
        }

        public async Task<RefundResponse> Handle(CreateRefundCommand request, CancellationToken cancellationToken)
        {
            var entity = new RefundRequest
            {
                    AmountOfMoney = new AmountOfMoney
                    {
                        Amount = (long)(request.orderAmount * 100),
                        CurrencyCode = "EUR"
                    },
                   
            };
            string PaymentId = request.PaymentId+"_0";
            return await _paymentService.CreateRefund(PaymentId, entity);
        }
    }
}
