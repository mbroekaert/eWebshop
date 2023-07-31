using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Cart.Queries
{
    public class GetSpecificProductsQuery : IRequest<IEnumerable<ProductResponseDto>>
    {
        public List<int> ProductIds { get; set; }
        public GetSpecificProductsQuery() 
        {
        }
        public GetSpecificProductsQuery(List<int> productIds)
        {
            ProductIds = productIds;
        }
    }
    public class GetSpecificProductsQueryHandler : IRequestHandler<GetSpecificProductsQuery, IEnumerable<ProductResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSpecificProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductResponseDto>> Handle(GetSpecificProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Product
                        .Where(p => request.ProductIds.Contains(p.ProductId))
                        .ToListAsync(cancellationToken);
            return products.Select(c => _mapper.Map<ProductResponseDto>(c)).ToList();
        }
    }
}
