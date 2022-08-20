using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries.GetCategories;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CategoryResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var response = await Mediator.Send(new GetCategoryQuery());
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CategoryResponseDto))]
        public async Task<IActionResult> GetCategoryAsync([FromRoute]int id)
        {
            var response = await Mediator.Send(new GetCategoryQuery());
            return Ok(response.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost]
        [Authorize("write:messages")]
        public async Task<ActionResult<int>> CreateCategory(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                var validationResponse = new RequestValidatorResponseDto()
                {
                    Validations = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               )
                };
                return BadRequest(new BadRequestResponseDto()
                {
                    AdditionnalData = validationResponse,
                    ErrorCode = 1001,
                    Message = "Validation error"
                });
            }
            return await Mediator.Send(command);
        }
        [HttpPut("{id}")]
        [Authorize("write:messages")]
        public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                var validationResponse = new RequestValidatorResponseDto()
                {
                    Validations = ModelState
                               .Where(x => x.Value.Errors.Count > 0)
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               )
                };
                return BadRequest(new BadRequestResponseDto()
                {
                    AdditionnalData = validationResponse,
                    ErrorCode = 1001,
                    Message = "Validation error"
                });
            }
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize("write:messages")]
        public async Task<ActionResult<int>> DeleteCategory(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return NoContent();
        }

    }
}
