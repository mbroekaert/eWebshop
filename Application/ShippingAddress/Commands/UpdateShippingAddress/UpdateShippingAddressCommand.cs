using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.ShippingAddress.Commands.UpdateShippingAddress
{
    public class UpdateShippingAddressCommand : IRequest
    {
        public int ShippingAddressId { get; set; }
        public string ShippingAddressStreetName { get; set; }
        public int ShippingAddressStreetNumber { get; set; }
        public string ShippingAddressCity { get; set; }
        public string ShippingAddressZip { get; set; }
        public string ShippingAddressCountry { get; set; }
        public string CustomerAuth0UserId { get; set; }
    }
    public class UpdateShippingAddressCommandHandler : IRequestHandler<UpdateShippingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateShippingAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShippingAddress.FindAsync(request.ShippingAddressId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ShippingAddress), request.ShippingAddressId);
            }
            entity.ShippingAddressStreetName = request.ShippingAddressStreetName;
            entity.ShippingAddressStreetNumber = request.ShippingAddressStreetNumber;
            entity.ShippingAddressCity = request.ShippingAddressCity;
            entity.ShippingAddressZip = request.ShippingAddressZip;
            entity.ShippingAddressCountry = request.ShippingAddressCountry;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
