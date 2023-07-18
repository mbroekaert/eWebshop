using Application.Categories.Commands.CreateCategory;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public string ProductReference { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductPicture { get; set; }
        public Category Category { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                ProductName = request.ProductName,
                ProductReference = request.ProductReference,
                ProductPrice = request.ProductPrice,
                productQuantity = request.ProductQuantity,
                productPicture = request.ProductPicture,
                Category = request.Category

            };

            _context.Product.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.ProductId;
        }
    }
}
