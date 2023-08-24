using Application.Tokens.Commands.CreateToken;
using Application.Tokens.Commands.DeleteToken;
using Application.Tokens.Commands.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<string>> CreateTokenAsync(CreateTokenCommand command)
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
        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenResponseDto[]))]
        [Authorize]
        public async Task<IActionResult> GetTokensAsync([FromRoute] string userId)
        {
            var response = await Mediator.Send(new GetTokenQuery());
            return Ok(response.Where(c => c.CustomerAuth0UserId == userId));
        }

        [HttpDelete("{tokenId}")]
        [Authorize]
        public async Task<ActionResult<(bool result, string content)>> DeleteTokenAsync(string tokenId)
        {
            await Mediator.Send(new DeleteTokenCommand { TokenId = tokenId });
            return NoContent();
        }
    }
}
