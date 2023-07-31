using Application.Cart.Queries;
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
    public class CartController : ApiController
    {

        [HttpGet]
        //[Authorize]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> GetSpecificProductsAsync([FromQuery] List<int> ProductIds)
        {
            var query = new GetSpecificProductsQuery(ProductIds);
            var response = await Mediator.Send(query);
            return Ok(response);
        }



    }
}
