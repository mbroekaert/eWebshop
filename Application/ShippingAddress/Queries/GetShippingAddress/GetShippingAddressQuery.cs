using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.ShippingAddress.Queries.GetShippingAddress
{
    public class GetShippingAddressQuery : IRequest<IEnumerable<ShippingAddressResponseDto>>
    {
    }

    public class GetShippingAddressQueryHandler : IRequestHandler<GetShippingAddressQuery, IEnumerable<ShippingAddressResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShippingAddressQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShippingAddressResponseDto>> Handle(GetShippingAddressQuery request, CancellationToken cancellationToken)
        {
            var shippingAddresses = await _context.ShippingAddress
                        .ToListAsync(cancellationToken);
            return shippingAddresses.Select(c => _mapper.Map<ShippingAddressResponseDto>(c)).ToList();
        }
    }
}
