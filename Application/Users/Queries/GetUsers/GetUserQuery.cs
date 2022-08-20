using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Users.Queries.GetUsers
{

    public class GetUserQuery : IRequest<IEnumerable<UserResponseDto>>
    {
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IEnumerable<UserResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Users
                        .ToListAsync(cancellationToken);
            return categories.Select(c => _mapper.Map<UserResponseDto>(c)).ToList();
        }
    }
}
