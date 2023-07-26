using Application.Auth0Users.Services;
using Application.Common.Interfaces;
using Application.Users.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IAuth0UserService auth0UserService;
        public CustomerController(ICustomerService customerService, IAuth0UserService auth0UserService)
        {
            this.customerService = customerService;
            this.auth0UserService = auth0UserService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Get customer details
        public async Task<IActionResult> ViewCustomerDetails ()
        {
            return View(await customerService.GetCustomerAsync());
        }
        #endregion
        #region Create a new customer

        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Create(Customer customer) 
        {
            User user = new User
            {
                UserEmail = customer.CustomerEmail,
                Password = customer.Password,
                UserName = customer.CustomerFirstName + " " + customer.CustomerLastName

            };
            var auth0Result = await auth0UserService.CreateAuth0UserAsync(user);
            if (auth0Result.success)
            {
                var dbResult = await customerService.CreateCustomerAsync(customer);
                if (dbResult.success)
                {
                    TempData["success"] = dbResult.content;
                }
                else TempData["error"] = dbResult.content;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}
