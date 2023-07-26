using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class CustomerUserProfile : Profile
    {
        public CustomerUserProfile()
        {
            CreateMap<Customer, User>();
        }
    }
}
