using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResponseDto>();
        }
    }
}
