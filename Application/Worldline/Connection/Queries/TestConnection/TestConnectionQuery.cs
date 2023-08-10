using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Worldline.Connection.Queries.TestConnection
{
    public class TestConnectionQuery : IRequest<string>
    {

        public TestConnectionQuery() 
        { 
        }
        public class TestConnectionQueryHandler : IRequestHandler<TestConnectionQuery, string>
        {
            private readonly IMapper _mapper;
            
            private IPaymentService _paymentService;

            public TestConnectionQueryHandler(IMapper mapper, IPaymentService paymentService)
            {
                _mapper = mapper;
                _paymentService = paymentService;
            }

            public async Task<string> Handle(TestConnectionQuery request, CancellationToken cancellationToken)
            {
               return await _paymentService.TestConnection();
            }
        }
    }
}
