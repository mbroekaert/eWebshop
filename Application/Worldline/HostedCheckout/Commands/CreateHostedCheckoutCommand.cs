using Application.Common.Interfaces;
using Application.Worldline.HostedCheckout.Commands;
using MediatR;
using OnlinePayments.Sdk.Domain;

namespace Application.Worldline.HostedCheckout.Commands
{
    public class CreateHostedCheckoutCommand : IRequest<CreateHostedCheckoutResponse>
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
        public string orderReference { get; set; }
        public string returnUrl { get; set; }
        //public Domain.Entities.BillingAddress billingAddress { get; set; }
        //public Domain.Entities.ShippingAddress shippingAddress { get; set; }
    }

    public class CreateHostedCheckoutCommandHandler : IRequestHandler<CreateHostedCheckoutCommand, CreateHostedCheckoutResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPaymentService _paymentService;

        public CreateHostedCheckoutCommandHandler(IApplicationDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;   
        }

        public async Task<CreateHostedCheckoutResponse> Handle(CreateHostedCheckoutCommand request, CancellationToken cancellationToken)
        {
            var entity = new CreateHostedCheckoutRequest
            {
                Order = new Order
                {
                    AmountOfMoney = new AmountOfMoney
                    {
                        Amount = (long)(request.amount * 100),
                        CurrencyCode = request.currencyCode
                    },
                    References = new OrderReferences
                    {
                        MerchantReference = request.orderReference
                    },
                    Customer = new Customer
                    {
                        //BillingAddress = new Address
                        //{
                        //    City = request.billingAddress.BillingAddressCity,
                        //    Zip = request.billingAddress.BillingAddressZip,
                        //    Street = request.billingAddress.BillingAddressStreetName,
                        //    HouseNumber = request.billingAddress.BillingAddressStreetNumber.ToString(),
                        //    CountryCode = request.billingAddress.BillingAddressCountry.Substring(0, 2)
                        //}
                    },
                    Shipping = new Shipping
                    {
                        //Address = new AddressPersonal
                        //{
                        //    City = request.shippingAddress.ShippingAddressCity,
                        //    Zip = request.shippingAddress.ShippingAddressZip,
                        //    Street = request.shippingAddress.ShippingAddressStreetName,
                        //    HouseNumber = request.shippingAddress.ShippingAddressStreetNumber.ToString(),
                        //    CountryCode = request.shippingAddress.ShippingAddressCountry.Substring(0, 2)

                        //}
                    }
                }

            };
            return await _paymentService.CreateHostedCheckout(entity);
        }
    }
}
