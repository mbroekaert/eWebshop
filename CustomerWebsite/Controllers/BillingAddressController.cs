using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class BillingAddressController : CoreController
    {
        private readonly IBillingAddressService _billingAddressService;
        public BillingAddressController (IBillingAddressService billingAddressService)
        {
            _billingAddressService = billingAddressService;
        }
        #region Get billing address
        public async Task<IActionResult> Index()
        {
            return View(await _billingAddressService.GetBillingAddressAsync());
        }
        #endregion
        #region Create a new billing address

        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        public async Task<ActionResult> Create (BillingAddress billingAddress)
        {
            billingAddress.CustomerAuth0UserId = Auth0UserId;
            var result = await _billingAddressService.CreateBillingAddressAsync(billingAddress);
            if (result.success)
            {
                TempData["success"] = result.content;
                return RedirectToAction("Index");
            }
            else TempData["error"] = result.content;
            return RedirectToAction("Index");
        }

        #endregion
        #region Update an existing billing address

        // GET

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult> Edit (int id)
        {
            return View(await _billingAddressService.EditBillingAddressAsync(id));
        }

        // Post

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<ActionResult> Update (BillingAddress billingAddress)
        {
            billingAddress.CustomerAuth0UserId = Auth0UserId;
            var result = await _billingAddressService.UpdateBillingAddressAsync(billingAddress);
            if (result.success)
            {
                TempData["success"] = result.content; return RedirectToAction("Index");

            }
            else TempData["error"] = result.content; return RedirectToAction("Index");
        }


        #endregion
        #region Delete a billing address

        // GET
        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult> Delete (int id)
        {
            return View (await _billingAddressService.GetBillingAddressToDeleteAsync(id));
        }

        // POST
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<ActionResult> Delete (BillingAddress billingAddress)
        {
            billingAddress.CustomerAuth0UserId = Auth0UserId;
            var result = await _billingAddressService.DeleteBillingAddressAsync(billingAddress); 
            if (result.success) 
            {
                TempData["success"] = result.content;
                return RedirectToAction("Index");
            }
            else TempData["error"] = result.content;
            return RedirectToAction("Index");
        }

        #endregion
    }
}
