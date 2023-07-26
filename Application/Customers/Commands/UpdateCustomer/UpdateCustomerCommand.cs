using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer

{
    public class UpdateCustomerCommand : IRequest
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public int CustomerPhone { get; set; }
        public int CustomerId { get; set; }
        public string Password { get; set; }

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
            entity.CustomerFirstName = request.CustomerFirstName;
            entity.CustomerLastName = request.CustomerLastName;
            entity.CustomerEmail = request.CustomerEmail;
            entity.CustomerPhone = request.CustomerPhone;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
