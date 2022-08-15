using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetCategories
{

    public class GetCategoryQuery : IRequest<CategoryVm>
    {
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryVm> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {

            return new CategoryVm
            {
                Lists = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.DisplayOrder)
                .ToListAsync(cancellationToken)
            };
        }
    }
}
