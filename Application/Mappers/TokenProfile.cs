using AutoMapper;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class TokenProfile : Profile
    {
        public TokenProfile() 
        {
            CreateMap<Domain.Entities.Token, TokenResponseDto>();
        }
    }
}
