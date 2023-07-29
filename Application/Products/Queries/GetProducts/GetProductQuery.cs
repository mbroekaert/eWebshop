using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductQuery : IRequest<IEnumerable<ProductResponseDto>>
    {
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Product
                        .ToListAsync(cancellationToken);
            return products.Select(c => _mapper.Map<ProductResponseDto>(c)).ToList();
        }
    }
}
