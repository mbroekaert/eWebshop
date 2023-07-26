using Application.Auth0Users.Commands.CreateAuth0User;
using Application.Auth0Users.Commands.DeleteAuth0User;
using Application.Auth0Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth0UserController : ApiController
    {
        private GetAuth0ManagementTokenService _service;

        public Auth0UserController(GetAuth0ManagementTokenService service)
        {
            _service = service; 
        }

        [HttpPost]
        //[Authorize("write:users")]
        public async Task<ActionResult<string>> CreateAuth0User(CreateAuth0UserCommand command)
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
            var result =  await Mediator.Send(command);
            return result.Item2;
        }
        [HttpPost("{id}")]
        [Authorize("write:users")]
        public async Task<ActionResult<int>> DeleteAuth0User(string id)
        {
            await Mediator.Send(new DeleteAuth0UserCommand { Id = id });
            return NoContent();
        }
    }
}
