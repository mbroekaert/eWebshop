using Application.ShippingAddress.Commands.CreateShippingAddress;
using Application.ShippingAddress.Commands.DeleteShippingAddress;
using Application.ShippingAddress.Commands.UpdateShippingAddress;
using Application.ShippingAddress.Queries.GetShippingAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ApiController
    {
        [HttpGet("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ShippingAddressResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetShippingAddressesAsync([FromRoute] string userId)
        {
            var response = await Mediator.Send(new GetShippingAddressQuery());
            return Ok(response.Where(c=>c.CustomerAuth0UserId == userId));
        }

        [HttpGet("{shippingAddressId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ShippingAddressResponseDto))]
        public async Task<IActionResult> GetShippingAddressAsync([FromRoute] int shippingAddressId)
        {
            var response = await Mediator.Send(new GetShippingAddressQuery());
            return Ok(response.FirstOrDefault(c => c.ShippingAddressId == shippingAddressId));
        }

        [HttpPost]
       // [Authorize("write:messages")]
        public async Task<ActionResult<string>> CreateShippingAddress(CreateShippingAddressCommand command)
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
        [HttpPut("{shippingAddressId}")]
        //[Authorize("write:messages")]
        public async Task<ActionResult> UpdateShippingAddress(int ShippingAddressId, UpdateShippingAddressCommand command)
        {
            if (ShippingAddressId != command.ShippingAddressId) return BadRequest();
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
        [HttpDelete("{shippingAddressId}")]
        //[Authorize("write:messages")]
        public async Task<ActionResult<int>> DeleteShippingAddressAddress(int shippingAddressId)  
        {
            await Mediator.Send(new DeleteShippingAddressCommand { ShippingAddressId = shippingAddressId });
            return NoContent();
        }

    }
}
