using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Categories.Queries.GetCategories
{  public class UserDto : IMapFrom<User>
{
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
       
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
        }
    }

}
