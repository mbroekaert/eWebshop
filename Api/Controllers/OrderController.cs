using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiController
    {

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
        public async Task<ActionResult<int>> DeleteProduct(int OrderId)
        {
            await Mediator.Send(new DeleteOrderCommand { OrderId = OrderId });
            return NoContent();
        }

    }
}
