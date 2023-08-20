using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.DetailOrders.Queries.GetDetailOrders
{
    public class GetDetailOrdersQuery : IRequest<IEnumerable<DetailOrdersResponseDto>>
    {
    }

    public class GetDetailOrdersQueryHandler : IRequestHandler<GetDetailOrdersQuery, IEnumerable<DetailOrdersResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetailOrdersResponseDto>> Handle(GetDetailOrdersQuery request, CancellationToken cancellationToken)
        {
            var detailOrders = await _context.DetailOrder
                        .ToListAsync(cancellationToken);
            return detailOrders.Select(c => _mapper.Map<DetailOrdersResponseDto>(c)).ToList();
        }
    }
}
