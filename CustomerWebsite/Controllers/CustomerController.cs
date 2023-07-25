using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewCustomerDetails ()
        {
            return View(await customerService.GetCustomerAsync());
        }
    }
}
