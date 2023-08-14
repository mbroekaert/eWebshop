using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Billing.Queries.GetPayment
{
    public class GetPaymentQuery : IRequest<IEnumerable<PaymentResponseDto>>
    {
    }

    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, IEnumerable<PaymentResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPaymentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentResponseDto>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payments = await _context.Payment
                        .ToListAsync(cancellationToken);
            return payments.Select(c => _mapper.Map<PaymentResponseDto>(c)).ToList();
        }
    }
}
