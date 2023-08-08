using Application.DetailOrders.Commands.CreateDetailOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailOrderController : ApiController
    {

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<int>> CreateDetailOrder (CreateDetailOrderCommand command)
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
