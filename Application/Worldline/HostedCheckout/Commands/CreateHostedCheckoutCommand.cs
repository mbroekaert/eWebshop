using Application.Common.Interfaces;
using Application.Worldline.HostedCheckout.Commands;
using MediatR;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Request;

namespace Application.Worldline.HostedCheckout.Commands
{
    public class CreateHostedCheckoutCommand : IRequest<CreateHostedCheckoutResponse>
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
        public string orderReference { get; set; }
        public string returnUrl { get; set; }
        public BillingAddressRequestDto billingAddress { get; set; }
        public ShippingAddressRequestDto shippingAddress { get; set; }
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
                        BillingAddress = new Address
                        {
                            City = request.billingAddress.billingAddressCity,
                            Zip = request.billingAddress.billingAddressZip,
                            Street = request.billingAddress.billingAddressStreetName,
                            HouseNumber = request.billingAddress.billingAddressStreetNumber.ToString(),
                            CountryCode = request.billingAddress.billingAddressCountry.Substring(0, 2)
                        }
                    },
                    Shipping = new Shipping
                    {
                        Address = new AddressPersonal
                        {
                            City = request.shippingAddress.ShippingAddressCity,
                            Zip = request.shippingAddress.ShippingAddressZip,
                            Street = request.shippingAddress.ShippingAddressStreetName,
                            HouseNumber = request.shippingAddress.ShippingAddressStreetNumber.ToString(),
                            CountryCode = request.shippingAddress.ShippingAddressCountry.Substring(0, 2)
                        }
                    }
                },
                HostedCheckoutSpecificInput = new HostedCheckoutSpecificInput
                {
                    ReturnUrl = request.returnUrl
                },
                CardPaymentMethodSpecificInput = new CardPaymentMethodSpecificInputBase
                {
                    AuthorizationMode = "SALE"
                }
            };
            return await _paymentService.CreateHostedCheckout(entity);
        }
    }
}
