using AutoMapper;
using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Mappers
{
    public class ToDoItemsProfile : Profile
    {
        public ToDoItemsProfile()
        {
            CreateMap<ToDoItem, ToDoItemResponseDto>();
        }
    }
}
