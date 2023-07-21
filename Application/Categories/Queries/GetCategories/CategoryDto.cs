using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Categories.Queries.GetCategories 
{ 
    public class CategoryDto : IMapFrom<Category>
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int CategoryDisplayOrder { get; set; }

        public DateTime CategoryCreatedDateTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>();
        }
    }
    
}
