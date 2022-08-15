﻿using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            return await _context.Categories
                            .Select(c => _mapper.Map<CategoryResponseDto>(c))
                            .OrderBy(t => t.DisplayOrder)
                            .ToListAsync(cancellationToken);
        }
    }
}
