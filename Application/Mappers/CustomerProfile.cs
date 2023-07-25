using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponseDto>();
        }
    }
}
