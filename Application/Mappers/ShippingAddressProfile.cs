using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class ShippingAddressProfile : Profile
    {
        public ShippingAddressProfile()
        {
            CreateMap<Domain.Entities.ShippingAddress, ShippingAddressResponseDto>();
        }
    }
}

