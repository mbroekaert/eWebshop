using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Response
{
    public class CartViewResponseDto
    {
        public Dictionary<int, int> CartItems { get; set; }
    }
}
