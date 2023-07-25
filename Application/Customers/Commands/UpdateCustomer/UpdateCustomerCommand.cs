using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer

{
    public class UpdateCustomerCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public int CustomerId { get; set; }

    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customer.FindAsync(request.CustomerId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }
            entity.CustomerFirstName = request.FirstName;
            entity.CustomerLastName = request.LastName;
            entity.CustomerEmail = request.Email;
            entity.CustomerPhone = request.Phone;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
