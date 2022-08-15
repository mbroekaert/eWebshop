using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Categories.Queries.GetCategories;
using Application.Categories.Commands.CreateCategory;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<CategoryVm>> GetCategory()
        {
            return await Mediator.Send(new GetCategoryQuery());
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
