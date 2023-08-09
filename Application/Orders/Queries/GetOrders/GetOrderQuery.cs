using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Orders.Queries.GetOrders
{
    public class GetOrderQuery : IRequest<IEnumerable<OrderResponseDto>>
    {
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IEnumerable<OrderResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponseDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Order
                        .ToListAsync(cancellationToken);
            return orders.Select(c => _mapper.Map<OrderResponseDto>(c)).ToList();
        }
    }
}
