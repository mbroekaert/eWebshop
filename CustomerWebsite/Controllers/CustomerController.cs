using Application.Auth0Users.Services;
using Application.Common.Interfaces;
using Application.Users.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerWebsite.Controllers
{
    public class CustomerController : CoreController
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
            string auth0UserId = Auth0UserId;
            return View(await customerService.GetCustomerAsync(auth0UserId));
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
                customer.Auth0UserId = auth0Result.content.Substring(1, 30);
                var dbResult = await customerService.CreateCustomerAsync(customer);
                if (dbResult.success)
                {
                    TempData["success"] = dbResult.content;
                }
                else TempData["error"] = dbResult.content;
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Product");
        }

        #endregion
    }
}
