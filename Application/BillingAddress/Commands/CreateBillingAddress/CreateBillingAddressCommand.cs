using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.BillingAddress.Commands.CreateBillingAddress
{
    public class CreateBillingAddressCommand : IRequest<string>
    {
        public string BillingAddressStreetName { get; set; }
        public int BillingAddressStreetNumber { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressZip { get; set; }
        public string BillingAddressCountry { get; set; }
        public string CustomerAuth0UserId { get; set; }

    }

    public class CreateBillingAddressCommandHandler : IRequestHandler<CreateBillingAddressCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateBillingAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateBillingAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.BillingAddress
            {
                BillingAddressStreetName = request.BillingAddressStreetName,
                BillingAddressStreetNumber = request.BillingAddressStreetNumber,
                BillingAddressCity = request.BillingAddressCity,
                BillingAddressZip = request.BillingAddressZip,
                BillingAddressCountry = request.BillingAddressCountry,
                CustomerAuth0UserId = request.CustomerAuth0UserId
            };

            _context.BillingAddress.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.CustomerAuth0UserId;
        }
    }
}

