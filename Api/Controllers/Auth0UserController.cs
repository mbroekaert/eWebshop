using Application.Auth0Users.Commands.CreateAuth0User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth0UserController : ApiController
    {
        [HttpPost]
        [Authorize("write:users")]
        public async Task<ActionResult<bool>> CreateAuth0User(CreateAuth0UserCommand command)
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
    }
}
