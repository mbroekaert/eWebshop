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
        [Authorize("read:users")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UserResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetUsersAsync()
        {
            var response = await Mediator.Send(new GetUserQuery());
            return Ok(response);
        }

        [HttpGet("{userId}")]
        [Authorize("read:users")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponseDto))]
        public async Task<IActionResult> GetUserAsync([FromRoute] int UserId)
        {
            var response = await Mediator.Send(new GetUserQuery());
            return Ok(response.FirstOrDefault(c => c.UserId == UserId));
        }

        [HttpPost]
        [Authorize("write:users")]
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

        [HttpPut("{userId}")]
        [Authorize("write:users")]
        public async Task<ActionResult> UpdateUser(int Userid, UpdateUserCommand command)
        {
            if (Userid != command.UserId) return BadRequest();
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
        [Authorize("write:users")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });
            return NoContent();
        }


    }
}
