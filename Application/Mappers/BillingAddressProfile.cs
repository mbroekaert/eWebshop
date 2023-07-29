using AutoMapper;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class BillingAddressProfile : Profile
    {
        public BillingAddressProfile() 
        {
            CreateMap<Domain.Entities.BillingAddress, BillingAddressResponseDto>();
        }
    }
}
