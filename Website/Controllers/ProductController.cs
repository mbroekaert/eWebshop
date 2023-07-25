using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;

namespace Website.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }


        #region Get products
        public async Task<IActionResult> Index()
        {
            return View(await productService.GetProductsAsync());
        }
        #endregion

        #region Create new product
        // GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await categoryService.GetCategoriesAsync();
            return View();
        }

        // POST
        public async Task<ActionResult> Create(Product product)
        {
            var result = await productService.CreateProductAsync(product);
            if (result.success)
            {
                TempData["success"] = result.content;
                return RedirectToAction("Index");
            }
            TempData["error"] = result.content;
            ViewBag.Categories = await categoryService.GetCategoriesAsync();
            return View(product);
        }
        #endregion

        #region Update product
        /* Note : Better to use patch instead of put
         * Will allow to manage updates separately, and not have to change "all" fields
         * when posting a request. Here Name & DisplayOrder must be updated in order for
         * the request to be accepted */

        // GET
        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = await categoryService.GetCategoriesAsync();
            return View(await productService.EditProductAsync(id));
        }

        // POST
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var result = await productService.UpdateProductAsync(product);
            if (result.success)
            {
                TempData["success"] = result.content;
            }
            else TempData["error"] = result.content;

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete product

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await productService.GetProductToDeleteAsync(id));
        }
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            var result = await productService.DeleteProductAsync(product);
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
