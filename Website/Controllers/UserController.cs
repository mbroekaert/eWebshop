using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Website.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #region Get users
        public async Task<IActionResult> Index()
        {
            return View(await userService.GetUsersAsync());
        }
        #endregion

        #region Create new user
        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST
        public async Task<ActionResult> Create(User user)
        {
            var result = await userService.CreateUserAsync(user);
            if (result.success)
            {
                TempData["success"] = result.content;
                return RedirectToAction("Index");
            }
            else TempData["error"] = result.content;

            return View(user);
        }
        #endregion

        #region Update user
        /* Note : Better to use patch instead of put
         * Will allow to manage updates separately, and not have to change "all" fields
         * when posting a request. Here Name & DisplayOrder must be updated in order for
         * the request to be accepted */

        // GET
        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await userService.EditUserAsync(id));
        }

        // POST
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var result = await userService.UpdateUserAsync(user);
            if (result.success)
            {
                TempData["success"] = result.content;
            }
            else TempData["error"] = result.content;

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete user

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await userService.GetUserToDeleteAsync(id));
        }
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            var result = await userService.DeleteUserAsync(user);
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
