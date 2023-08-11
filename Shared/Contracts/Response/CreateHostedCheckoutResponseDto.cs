using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Response
{
    public  class CreateHostedCheckoutResponseDto
    {
        public string returnmac { get; set; }
        public string hostedCheckoutId { get; set; }
        public IList<string> invalidTokens { get; set; }
        public string merchantReference { get; set; }
        public string partialRedirectUrl { get; set; }
        public string redirectUrl { get; set; }
    }
}
