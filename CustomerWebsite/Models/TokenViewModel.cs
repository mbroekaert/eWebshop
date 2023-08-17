using Shared.Contracts.Response;

namespace CustomerWebsite.Models
{
    public class TokenViewModel
    {
        public TokenResponseDto[] Tokens { get; set; }
        public Dictionary<int, string> PaymentProductMapping { get; set; }
    }
}
