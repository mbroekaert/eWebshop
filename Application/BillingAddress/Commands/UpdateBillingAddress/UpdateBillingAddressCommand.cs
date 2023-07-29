using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.BillingAddress.Commands.UpdateBillingAddress
{
    public class UpdateBillingAddressCommand : IRequest
    {
        public int BillingAddressId { get; set; }
        public string BillingAddressStreetName { get; set; }
        public int BillingAddressStreetNumber { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressZip { get; set; }
        public string BillingAddressCountry { get; set; }
        public string CustomerAuth0UserId { get; set; }
    }
    public class UpdateBillingAddressCommandHandler : IRequestHandler<UpdateBillingAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBillingAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBillingAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BillingAddress.FindAsync(request.BillingAddressId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BillingAddress), request.BillingAddressId);
            }
            entity.BillingAddressStreetName = request.BillingAddressStreetName;
            entity.BillingAddressStreetNumber = request.BillingAddressStreetNumber;
            entity.BillingAddressCity = request.BillingAddressCity;
            entity.BillingAddressZip = request.BillingAddressZip;
            entity.BillingAddressCountry = request.BillingAddressCountry;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
