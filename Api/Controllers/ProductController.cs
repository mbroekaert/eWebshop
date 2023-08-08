using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetCategories;
using Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiController
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ProductResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetProductsAsync()
        {
            var response = await Mediator.Send(new GetProductQuery());
            return Ok(response);
        }

        [HttpGet("{productId}")]
        //[Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductResponseDto))]
        public async Task<IActionResult> GetProductAsync([FromRoute] int productId)
        {
            var response = await Mediator.Send(new GetProductQuery());
            return Ok(response.FirstOrDefault(c => c.ProductId == productId));
        }

        [HttpPost]
        [Authorize("write:messages")]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
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
        [HttpPut("{ProductId}")]
        //[Authorize("write:messages")]
        public async Task<ActionResult> UpdateProduct(int ProductId, UpdateProductCommand command)
        {
            if (ProductId != command.ProductId) return BadRequest();
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
        [HttpDelete("{ProductId}")]
        [Authorize("write:messages")]
        public async Task<ActionResult<int>> DeleteProduct(int ProductId)  
        {
            await Mediator.Send(new DeleteProductCommand { ProductId = ProductId });
            return NoContent();
        }

    }
}
