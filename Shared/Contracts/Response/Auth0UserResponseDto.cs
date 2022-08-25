using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Response
{
    public class Auth0UserResponseDto
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
