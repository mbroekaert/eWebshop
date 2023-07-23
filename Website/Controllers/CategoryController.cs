using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        #region Get categories
        public async Task<IActionResult> Index()
        {
            return View(await categoryService.GetCategoriesAsync());
        }
        #endregion

        #region Create new category
        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST
        public async Task<ActionResult> Create(Category category)
        {
            var result = await categoryService.CreateCategoryAsync(category);
            if (result.success)
            {
                TempData["success"] = result.content;
                return RedirectToAction("Index");
            }
            else TempData["error"] = result.content;

            return View(category);
        }
        #endregion

        #region Update category
        /* Note : Better to use patch instead of put
         * Will allow to manage updates separately, and not have to change "all" fields
         * when posting a request. Here Name & DisplayOrder must be updated in order for
         * the request to be accepted */

        // GET
        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await categoryService.EditCategoryAsync(id));
        }

        // POST
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var result = await categoryService.UpdateCategoryAsync(category);
            if (result.success)
            {
                TempData["success"] = result.content;
            }
            else TempData["error"] = result.content;
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete category

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await categoryService.GetCategoryToDeleteAsync(id));
        }
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            var result = await categoryService.DeleteCategoryAsync(category);
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
