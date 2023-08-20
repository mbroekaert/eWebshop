using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.UpdateOrder;
using Application.Orders.Queries.GetOrders;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiController
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var response = await Mediator.Send(new GetOrderQuery());
            return Ok(response);
        }

        [HttpGet("{customerId}")]
        //[Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetCustomerOrdersAsync([FromRoute] string customerId)
        {
            var response = await Mediator.Send(new GetOrderQuery());
            return Ok(response.Where(c => c.CustomerAuth0UserId == customerId));
        }

        [HttpGet("order/{orderId}")]
        //[Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            var response = await Mediator.Send(new GetOrderQuery());
            return Ok(response.Where(c => c.OrderId == orderId));
        }

        [HttpGet("reference/{orderReference}")]
        //[Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetOrderByOrderReference([FromRoute] string orderReference)
        {
            var response = await Mediator.Send(new GetOrderQuery());
            return Ok(response.Where(c => c.OrderReference == orderReference));
        }


        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderCommand command)
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

        [HttpDelete("{OrderId}")]
        public async Task<ActionResult<int>> DeleteOrder(int OrderId)
        {
            await Mediator.Send(new DeleteOrderCommand { OrderId = OrderId });
            return NoContent();
        }

        [HttpPut("{OrderId}")]
        public async Task<ActionResult> UpdateOrderStatus (int orderId, UpdateOrderCommand command)
        {
            if (orderId != command.OrderId) return BadRequest();
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
