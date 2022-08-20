using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
            [HttpGet]
            [Authorize("read:messages")]
            [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UserResponseDto>))]
            [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
            public async Task<IActionResult> GetUsersAsync()
            {
                var response = await Mediator.Send(new GetUserQuery());
                return Ok(response);
            }

            [HttpGet("{id}")]
            [Authorize("read:messages")]
            [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponseDto))]
            public async Task<IActionResult> GetUserAsync([FromRoute] int id)
            {
                var response = await Mediator.Send(new GetUserQuery());
                return Ok(response.FirstOrDefault(c => c.Id == id));
            }

            [HttpPost]
            [Authorize("write:messages")]
            public async Task<ActionResult<int>> CreateUser(CreateUserCommand command)
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
            public async Task<ActionResult> UpdateUser(int id, UpdateUserCommand command)
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
            public async Task<ActionResult<int>> DeleteUser(int id)
            {
                await Mediator.Send(new DeleteUserCommand { Id = id });
                return NoContent();
            }

        
    }
}
