using Application.Worldline.Connection.Queries.TestConnection;
using Application.Worldline.HostedCheckout.Commands;
using Application.Worldline.Refund.Commands;
using Microsoft.AspNetCore.Mvc;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Response;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ApiController
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponseDto))]
        public async Task<IActionResult> TestConnectionAsync()
        {
            var response = await Mediator.Send(new TestConnectionQuery());
            return Ok(response);
        }

        [HttpPost]
        public async Task<CreateHostedCheckoutResponse> CreateHostedCheckoutAsync(CreateHostedCheckoutCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("refund")]
        public async Task<RefundResponse> CreateRefundAsync(CreateRefundCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
