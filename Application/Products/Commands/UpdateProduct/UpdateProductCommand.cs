using Application.Categories.Commands.UpdateCategory;
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
        public string ProductPicture { get; set; }
        public Category Category { get; set; }
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
            entity.productPicture = request.ProductPicture;
            entity.ProductReference = request.ProductReference;
            entity.productQuantity = request.ProductQuantity;
            entity.Category = request.Category;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
