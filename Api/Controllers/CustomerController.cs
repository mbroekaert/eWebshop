using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiController
    {
        //[HttpGet]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CustomerResponseDto>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        //public async Task<IActionResult> GetCustomerAsync()
        //{
        //    var response = await Mediator.Send(new GetCustomerQuery());
        //    return Ok(response);
        //}

        
        [HttpGet("{auth0UserId}")]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CustomerResponseDto))]
        public async Task<IActionResult> GetCustomerAsync(string auth0UserId)
        {
            var response = await Mediator.Send(new GetCustomerQuery());

            return Ok(response.FirstOrDefault(c => c.Auth0UserId == auth0UserId));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(CreateCustomerCommand command)
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
        [Authorize]
        public async Task<ActionResult> UpdateCustomer(int id, UpdateCustomerCommand command)
        {
            if (id != command.CustomerId) return BadRequest();
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
    }
}
