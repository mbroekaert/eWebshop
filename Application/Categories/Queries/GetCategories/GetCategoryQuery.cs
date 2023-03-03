using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Categories.Queries.GetCategories
{

    public class GetCategoryQuery : IRequest<IEnumerable<CategoryResponseDto>>
    {
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IEnumerable<CategoryResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
                var categories = await _context.Category
                            .OrderBy(t => t.CategoryDisplayOrder)
                            .ToListAsync(cancellationToken);
                return categories.Select(c => _mapper.Map<CategoryResponseDto>(c)).ToList();
        }
    }
}
