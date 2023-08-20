using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class DetailOrdersProfile : Profile
    {
        public DetailOrdersProfile() 
        {
            CreateMap<DetailOrder, DetailOrderResponseDto>();
        }
        
    }
}
