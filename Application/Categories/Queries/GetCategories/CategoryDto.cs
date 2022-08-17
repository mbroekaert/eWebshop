using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Categories.Queries.GetCategories
{ /* to delete */
    public class CategoryDto : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>();
        }
    }
    
}
