﻿using Application.Billing.Commands.CreateBilling;
using Application.Billing.Commands.UpdateBilling;
using Application.Billing.Queries.GetPayment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreatePaymentAsync(CreateBillingCommand command)
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

        [HttpGet("{paymentPayid}")]
        public async Task<PaymentResponseDto> GetPaymentByPayid (string paymentPayid)
        {
            var response = await Mediator.Send(new GetPaymentQuery());
            var tempResponse = response.FirstOrDefault(c => c.PaymentPayid == paymentPayid);
            return response.FirstOrDefault(c => c.PaymentPayid == paymentPayid);
        }
        [HttpGet("order/{orderId}")]
        public async Task<PaymentResponseDto> GetPaymentByOrderId(int orderId)
        {
            var response = await Mediator.Send(new GetPaymentQuery());
            var tempResponse = response.FirstOrDefault(c => c.OrderId == orderId);
            return response.FirstOrDefault(c => c.OrderId == orderId);
        }


        /* Check payment status to update order confirmation page*/
        [HttpGet("confirm/GetPaymentByPayidFromQuery")]
        
        public async Task<PaymentResponseDto> GetPaymentByPayidFromQuery()
        {
            string paymentPayid = HttpContext.Request.Query["paymentPayid"].ToString();

            var response = await Mediator.Send(new GetPaymentQuery());
            return response.FirstOrDefault(c => c.PaymentPayid == paymentPayid);
        }

        [HttpPut("{paymentId}")]
        
        public async Task<ActionResult> UpdatePayment (UpdateBillingCommand command, int paymentId)
        {
            if (paymentId != command.PaymentId) return BadRequest();
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
