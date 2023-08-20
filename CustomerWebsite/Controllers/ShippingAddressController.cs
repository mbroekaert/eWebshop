using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Services;

namespace CustomerWebsite.Controllers
{
    public class ShippingAddressController : CoreController
    {
        private readonly IShippingAddressService _shippingAddressService;

        public ShippingAddressController (IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
        }
        #region Get shipping address
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = Auth0UserId;
            return View(await _shippingAddressService.GetShippingAddressAsync(userId));
        }
        #endregion
        #region Create a new shipping address
        //GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        public async Task<ActionResult> Create (ShippingAddress shippingAddress)
        {
            shippingAddress.CustomerAuth0UserId = Auth0UserId;
            var result = await _shippingAddressService.CreateShippingAddressAsync(shippingAddress);
            if (result.success)
            {
                TempData["success"] = result.content;
                return RedirectToAction("Index");
            }
            TempData["error"] = result.content;
            return View(shippingAddress);
        }
        #endregion
        #region Update an existing shipping address

        // GET

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _shippingAddressService.EditShippingAddressAsync(id));
        }

        // POST

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<ActionResult> Edit (ShippingAddress shippingAddress)
        {
            shippingAddress.CustomerAuth0UserId= Auth0UserId;
            var result = await _shippingAddressService.UpdateShippingAddressAsync(shippingAddress);
            if (result.success)
            {
                TempData["success"] = result.content;
            }
            else TempData["error"] = result.content;

            return RedirectToAction("Index");
        }

        #endregion
        #region Delete a shipping address
        // GET
        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _shippingAddressService.GetShippingAddressToDeleteAsync(id));
        }
        // DELETE
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<ActionResult> Delete (ShippingAddress shippingAddress)
        {
            shippingAddress.CustomerAuth0UserId = Auth0UserId;
            var result = await _shippingAddressService.DeleteShippingAddressAsync(shippingAddress);
            if (result.success)
            {
                TempData["success"] = result.content;
            }
            else TempData["error"] = result.content;
            return RedirectToAction("Index");
        }

        #endregion

    }
}
