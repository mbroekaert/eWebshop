using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.BillingAddress.Queries.UpdateBillingAddress
{
    public class GetBillingAddressQuery : IRequest<IEnumerable<BillingAddressResponseDto>>
    {
    }

    public class GetBillingAddressQueryHandler : IRequestHandler<GetBillingAddressQuery, IEnumerable<BillingAddressResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBillingAddressQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BillingAddressResponseDto>> Handle(GetBillingAddressQuery request, CancellationToken cancellationToken)
        {
            var billingAddresses = await _context.BillingAddress
                        .ToListAsync(cancellationToken);
            return billingAddresses.Select(c => _mapper.Map<BillingAddressResponseDto>(c)).ToList();
        }
    }
}
