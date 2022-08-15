using Application.Categories.Commands.CreateCategory;
using Application.Categories.Queries.GetCategories;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CategoryResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BadRequestResponseDto()
                {
                    AdditionnatData = ModelState.Values,
                    ErrorCode = 1001,
                    Message = "Error validating model"
                });
            }

            var response = await Mediator.Send(new GetCategoryQuery());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
