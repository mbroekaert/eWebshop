using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponseDto>();
        }
    }
}
