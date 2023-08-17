using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tokens.Commands.Queries
{
   
        public class GetTokenQuery : IRequest<IEnumerable<TokenResponseDto>>
        {
        }

        public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, IEnumerable<TokenResponseDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetTokenQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<TokenResponseDto>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
            {
                var tokens = await _context.Token
                            .ToListAsync(cancellationToken);
                return tokens.Select(c => _mapper.Map<TokenResponseDto>(c)).ToList();
            }
        }
}
