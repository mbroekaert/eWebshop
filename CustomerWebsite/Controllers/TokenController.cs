using Application.Common.Interfaces;
using CustomerWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class TokenController : CoreController
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            /* Create mapping for paymentProductId */
            var paymentProductMapping = new Dictionary<int, string>
            {
                { 1, "Visa" },
                { 2, "American Express" },
                { 3, "Mastercard" },
                { 130, "Carte Bancaire" },
                { 132, "Diners Club" },
                { 128, "Discover" },
                { 840, "Paypal" }
            };
            TokenViewModel mapping = new TokenViewModel
            {
                Tokens = await _tokenService.GetTokensAsync(Auth0UserId),
                PaymentProductMapping = paymentProductMapping
            }; 
            return View(mapping);
        }
    }
}
