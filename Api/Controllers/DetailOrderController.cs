using Application.DetailOrders.Commands.CreateDetailOrder;
using Application.DetailOrders.Commands.DeleteDetailsOrder;
using Application.DetailOrders.Queries.GetDetailOrders;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailOrderController : ApiController
    {

        [HttpPost]
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
        [HttpGet("orderId")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DetailOrdersResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetDetailOrdersDetail ([FromRoute] int orderId)
        {
            var response = await Mediator.Send(new GetDetailOrdersQuery());
            return Ok(response.Where(c => c.OrderId == orderId));
        }

        [HttpDelete("detailOrder/{orderId}")]
        public async Task<ActionResult<int>> DeleteProduct(int orderId)
        {
            await Mediator.Send(new DeleteDetailOrdersCommand { OrderId = orderId });
            return NoContent();
        }
    }
}
