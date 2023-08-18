using Application.BillingAddress.Commands.CreateBillingAddress;
using Application.BillingAddress.Commands.DeleteBillingAddress;
using Application.BillingAddress.Commands.UpdateBillingAddress;
using Application.BillingAddress.Queries.UpdateBillingAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingAddressController : ApiController
    {
        [HttpGet("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<BillingAddressResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetBillingAddressesAsync([FromRoute]string userId)
        {
            var response = await Mediator.Send(new GetBillingAddressQuery());
            return Ok(response.Where(c=>c.CustomerAuth0UserId == userId));
        }

        [HttpGet("{billingAddressId}")]
        //[Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BillingAddressResponseDto))]
        public async Task<IActionResult> GetBillingAddressAsync([FromRoute] int billingAddressId)
        {
            var response = await Mediator.Send(new GetBillingAddressQuery());
            return Ok(response.FirstOrDefault(c => c.BillingAddressId == billingAddressId));
        }

        [HttpPost]
        //[Authorize("write:messages")]
        public async Task<ActionResult<string>> CreateBillingAddress(CreateBillingAddressCommand command)
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
        [HttpPut("{billingAddressId}")]
       // [Authorize("write:messages")]
        public async Task<ActionResult> UpdateBillingAddress(int BillingAddressId, UpdateBillingAddressCommand command)
        {
            if (BillingAddressId != command.BillingAddressId) return BadRequest();
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
        [HttpDelete("{billingAddressId}")]
        //[Authorize("write:messages")]
        public async Task<ActionResult<int>> DeleteBillingAddress(int billingAddressId)  
        {
            await Mediator.Send(new DeleteBillingAddressCommand { BillingAddressId = billingAddressId });
            return NoContent();
        }

    }
}
