using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            {
                CustomerFirstName = request.FirstName,
                CustomerLastName = request.LastName,
                CustomerEmail = request.Email,
                CustomerPhone = request.Phone
            };

            _context.Customer.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.CustomerId;
        }
    }
}

