using Application.Products.Commands.UpdateProduct;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int CategoryId { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Product.FindAsync(request.ProductId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }
            entity.ProductName = request.ProductName;
            entity.ProductPrice = request.ProductPrice;
            entity.ProductReference = request.ProductReference;
            entity.ProductQuantity = request.ProductQuantity;
            entity.CategoryId = request.CategoryId;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
