using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Products.Queries.GetCategories
{
    public class ProductDto : IMapFrom<Product>
    {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductReference { get; set; }
            public double ProductPrice { get; set; }
            public int ProductQuantity { get; set; }
            public int CategoryId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
