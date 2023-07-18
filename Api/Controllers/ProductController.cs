﻿using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetCategories;
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
        [Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ProductResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetProductsAsync()
        {
            var response = await Mediator.Send(new GetProductQuery());
            return Ok(response);
        }

        [HttpGet("{ProductId}")]
        [Authorize("read:messages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductResponseDto))]
        public async Task<IActionResult> GetProductAsync([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetProductQuery());
            return Ok(response.FirstOrDefault(c => c.ProductId == id));
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
        [HttpPut("{id}")]
        [Authorize("write:messages")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.ProductId) return BadRequest();
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
        [HttpDelete("{id}")]
        [Authorize("write:messages")]
        public async Task<ActionResult<int>> DeleteProduct(int id)
        {
            await Mediator.Send(new DeleteProductCommand { ProductId = id });
            return NoContent();
        }

    }
}
