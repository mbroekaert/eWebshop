using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Request;

namespace CustomerWebsite.Controllers
{
    public class PaymentController : CoreController
    {
        private readonly IBillingService _billingService;
        private readonly IWebhookService _webhookService;
        private readonly IOrderService _orderService;
        private readonly ITokenService _tokenService;
        private readonly IDetailOrdersService _detailOrdersService;
        private readonly IProductService _productService;

        public PaymentController(IBillingService billingService, IWebhookService webhookService, IOrderService orderService, ITokenService tokenService, IDetailOrdersService detailOrdersService, IProductService productService)
        {
            _billingService = billingService;
            _webhookService = webhookService;
            _orderService = orderService;
            _tokenService = tokenService;
            _detailOrdersService = detailOrdersService;
            _productService = productService;
        }

        public async Task<IActionResult> OrderConfirmation([FromQuery] string RETURNMAC, int HostedCheckoutId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var result = await _webhookService.HandleWebhook(Request);

            /* Distinguish between payments and refunds */

            if (result.Payment is not null)
            {
                /* Retrieve order details */
                var orderResult = await _orderService.GetOrderByOrderReference(result.Payment.PaymentOutput.References.MerchantReference);
                var order = orderResult[0];

                /* Check if payment exists */
                bool payment = await _billingService.CheckPaymentExistence(result.Payment.Id.Substring(0, 10));

                /* If no, create a new one */
                if (payment == false)
                {
                    /* If status = 0, 4, 51, 5, 91, 99, 9 create a new payment */
                    switch (result.Payment.StatusOutput.StatusCode)
                    {
                        case 0:
                        case 4:
                        case 51:
                        case 91:
                        case 99:
                        case 9:
                        case 5:
                            /* Create Payment Dto */
                            PaymentRequestDto paymentRequestDto = new PaymentRequestDto
                            {
                                PaymentPayid = result.Payment.Id.Substring(0, 10),
                                PaymentReference = result.Payment.PaymentOutput.References.MerchantReference,
                                PaymentStatus = result.Payment.StatusOutput.StatusCode,
                                OrderId = order.OrderId
                            };
                            /* Send to service */
                            var paymentResult = await _billingService.CreatePayment(paymentRequestDto);
                            /* Update order status on a status 9 */
                            if (result.Payment.StatusOutput.StatusCode == 9)
                            {
                                var orderUpdateResult = await _orderService.UpdateOrderStatus(order, "Paid");
                            }
                            /* Create token on successful status */
                            if (result.Payment.StatusOutput.StatusCode == 5 || result.Payment.StatusOutput.StatusCode == 9)
                            {
                                /* Handle credit card token */
                                if (result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput is not null && result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Token is not null)
                                {
                                    TokenRequestDto token = new TokenRequestDto
                                    {
                                        TokenId = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Token,
                                        PaymentProductId = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.PaymentProductId,
                                        CardNumber = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Card.CardNumber,
                                        ExpiryDate = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Card.ExpiryDate,
                                        CustomerAuth0UserId = order.CustomerAuth0UserId
                                    };
                                    var tokenResponse = await _tokenService.CreateTokenAsync(token);
                                }
                            }
                            break;
                        /* If status = 1 (cancelled), delete order, restock products & delete orderDetails */
                        case 1:
                            Domain.Entities.Order orderToDelete = new Domain.Entities.Order
                            {
                                OrderId = order.OrderId,
                                OrderReference = order.OrderReference,
                                OrderAmount = order.OrderAmount,
                                OrderDate = order.OrderDate,
                                Status = order.Status,
                                CustomerAuth0UserId = order.CustomerAuth0UserId,
                                BillingAddressId = order.BillingAddressId,
                                ShippingAddressId = order.ShippingAddressId
                            };
                            var deleteOrderResult = await _orderService.DeleteOrderAsync(orderToDelete);
                            /* get orderDetails */
                            var detailsResult = await _detailOrdersService.GetDetailsOrder(order.OrderId);
                            /* Update stock */
                            foreach (var details in detailsResult)
                            {
                                /* Retrieve product details */
                                var productResult = await _productService.EditProductAsync(details.ProductId);
                                /* Update existing product with new quantity */
                                var product = new Product
                                {
                                    ProductId = productResult.ProductId,
                                    ProductName = productResult.ProductName,
                                    ProductReference = productResult.ProductReference,
                                    ProductPrice = productResult.ProductPrice,
                                    ProductQuantity = productResult.ProductQuantity + details.Quantity,
                                    CategoryId = productResult.CategoryId,
                                };
                                var updatedProduct = await _productService.UpdateProductAsync(product);
                            }
                            /* Delete order details */
                            var deleteDetails = await _detailOrdersService.DeleteDetailsOrder(order.OrderId);
                            break;
                    }
                }
                /* If yes, update existing payment */
                else
                {
                    /* Retrieve payment data */
                    var paymentDetails = await _billingService.GetPaymentByPaymentPayid(result.Payment.Id.Substring(0, 10));

                    /* If paymentStatus is 0/5/51, update it to final status */
                    switch (paymentDetails.PaymentStatus)
                    {
                        case 0:
                        case 51:
                        case 5:
                            PaymentRequestDto paymentRequestDto = new PaymentRequestDto
                            {
                                PaymentId = paymentDetails.PaymentId,
                                PaymentPayid = result.Payment.Id.Substring(0, 10),
                                PaymentReference = result.Payment.PaymentOutput.References.MerchantReference,
                                PaymentStatus = result.Payment.StatusOutput.StatusCode,
                                OrderId = order.OrderId
                            };
                            /* Send to service */
                            var paymentResult = await _billingService.UpdatePaymentAsync(paymentRequestDto);
                            /* Update order status on a status 9 */
                            if (result.Payment.StatusOutput.StatusCode == 9)
                            {
                                var orderUpdateResult = await _orderService.UpdateOrderStatus(order, "Paid");
                            }
                            /* Create token on successful status */
                            if (result.Payment.StatusOutput.StatusCode == 5 || result.Payment.StatusOutput.StatusCode == 9)
                            {
                                /* Handle credit card token */
                                if (result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Token is not null)
                                {
                                    TokenRequestDto token = new TokenRequestDto
                                    {
                                        TokenId = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Token,
                                        PaymentProductId = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.PaymentProductId,
                                        CardNumber = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Card.CardNumber,
                                        ExpiryDate = result.Payment.PaymentOutput.CardPaymentMethodSpecificOutput.Card.ExpiryDate,
                                        CustomerAuth0UserId = order.CustomerAuth0UserId
                                    };
                                    var tokenResponse = await _tokenService.CreateTokenAsync(token);
                                }
                            }
                            break;

                        /* Manage refunds */
                        case 9:
                            if (result.Payment.StatusOutput.StatusCode == 8 || result.Payment.StatusOutput.StatusCode == 81)
                            {
                                PaymentRequestDto refundRequestDto = new PaymentRequestDto
                                {
                                    PaymentId = paymentDetails.PaymentId,
                                    PaymentPayid = result.Payment.Id.Substring(0, 10),
                                    PaymentReference = result.Payment.PaymentOutput.References.MerchantReference,
                                    PaymentStatus = result.Payment.StatusOutput.StatusCode,
                                    OrderId = order.OrderId
                                };
                                /* Send to service */
                                var refundResult = await _billingService.UpdatePaymentAsync(refundRequestDto);
                                /* Update order on a status 8/81 */
                                var orderUpdateResult = await _orderService.UpdateOrderStatus(order, "Refunded");
                            };
                            break;
                        /* If status = 1 (cancelled), delete order, restock products & delete orderDetails */
                        case 1:
                            Domain.Entities.Order orderToDelete = new Domain.Entities.Order
                            {
                                OrderId = order.OrderId,
                                OrderReference = order.OrderReference,
                                OrderAmount = order.OrderAmount,
                                OrderDate = order.OrderDate,
                                Status = order.Status,
                                CustomerAuth0UserId = order.CustomerAuth0UserId,
                                BillingAddressId = order.BillingAddressId,
                                ShippingAddressId = order.ShippingAddressId
                            };
                            var deleteOrderResult = await _orderService.DeleteOrderAsync(orderToDelete);
                            /* get orderDetails */
                            var detailsResult = await _detailOrdersService.GetDetailsOrder(order.OrderId);
                            /* Update stock */
                            foreach (var details in detailsResult)
                            {
                                /* Retrieve product details */
                                var productResult = await _productService.EditProductAsync(details.ProductId);
                                /* Update existing product with new quantity */
                                var product = new Product
                                {
                                    ProductId = productResult.ProductId,
                                    ProductName = productResult.ProductName,
                                    ProductReference = productResult.ProductReference,
                                    ProductPrice = productResult.ProductPrice,
                                    ProductQuantity = productResult.ProductQuantity + details.Quantity,
                                    CategoryId = productResult.CategoryId,
                                };
                                var updatedProduct = await _productService.UpdateProductAsync(product);
                            }
                            /* Delete order details */
                            var deleteDetails = await _detailOrdersService.DeleteDetailsOrder(order.OrderId);
                            break;

                    }

                }
            }
            if (result.Refund is not null)
            {
                /* Retrieve payment details */
                var paymentDetails = await _billingService.GetPaymentByPaymentPayid(result.Refund.Id.Substring(0, 10));

                /* Retrieve order details */
                var orderResult = await _orderService.GetOrderByOrderReference(result.Refund.RefundOutput.References.MerchantReference);
                var order = orderResult[0];

                if (result.Refund.StatusOutput.StatusCode == 8 || result.Refund.StatusOutput.StatusCode == 81)
                {
                    PaymentRequestDto refundRequestDto = new PaymentRequestDto
                    {
                        PaymentId = paymentDetails.PaymentId,
                        PaymentPayid = result.Refund.Id.Substring(0, 10),
                        PaymentReference = result.Refund.RefundOutput.References.MerchantReference,
                        PaymentStatus = result.Refund.StatusOutput.StatusCode,
                        OrderId = order.OrderId
                    };
                    /* Send to service */
                    var refundResult = await _billingService.UpdatePaymentAsync(refundRequestDto);
                    /* Update order on a status 8/81 */
                    var orderUpdateResult = await _orderService.UpdateOrderStatus(order, "Refunded");
                };
            }
            return Ok();
        }

    }
}
