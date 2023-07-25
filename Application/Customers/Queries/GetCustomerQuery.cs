using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Response;

namespace Application.Users.Queries.GetUsers
{

    public class GetCustomerQuery : IRequest<IEnumerable<CustomerResponseDto>>
    {
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, IEnumerable<CustomerResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponseDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customer
                        .ToListAsync(cancellationToken);
            return customers.Select(c => _mapper.Map<CustomerResponseDto>(c)).ToList();
        }
    }
}
