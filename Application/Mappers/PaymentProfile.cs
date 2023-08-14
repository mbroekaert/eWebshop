using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Request;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile() 
        {
            CreateMap<Payment, PaymentRequestDto>();
            CreateMap<Payment, PaymentResponseDto>();
        }
        
    }
}
