using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ShippingAddress.Commands.CreateShippingAddress
{
    public class CreateShippingAddressCommand : IRequest<string>
    {
        public string ShippingAddressStreetName { get; set; }
        public int ShippingAddressStreetNumber { get; set; }
        public string ShippingAddressCity { get; set; }
        public string ShippingAddressZip { get; set; }
        public string ShippingAddressCountry { get; set; }
        public string CustomerAuth0UserId { get; set; }

    }

    public class CreateShippingAddressCommandHandler : IRequestHandler<CreateShippingAddressCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateShippingAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.ShippingAddress
            {
                ShippingAddressStreetName = request.ShippingAddressStreetName,
                ShippingAddressStreetNumber = request.ShippingAddressStreetNumber,
                ShippingAddressCity = request.ShippingAddressCity,
                ShippingAddressZip = request.ShippingAddressZip,
                ShippingAddressCountry = request.ShippingAddressCountry,
                CustomerAuth0UserId = request.CustomerAuth0UserId
            };

            _context.ShippingAddress.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.CustomerAuth0UserId;
        }
    }
}

