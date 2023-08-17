using Application.Common.Interfaces;
using MediatR;

namespace Application.Tokens.Commands.CreateToken
{
    public class CreateTokenCommand : IRequest<string>
    {
        public string TokenId { get; set; }
        public int PaymentProductId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CustomerAuth0UserId { get; set; }

    }

    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateTokenCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Token
            {
                TokenId = request.TokenId,
                PaymentProductId = request.PaymentProductId,
                CardNumber = request.CardNumber,
                ExpiryDate = request.ExpiryDate,
                CustomerAuth0UserId = request.CustomerAuth0UserId
            };

            _context.Token.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.TokenId;
        }
    }
}

