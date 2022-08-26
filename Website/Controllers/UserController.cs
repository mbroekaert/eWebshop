using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Website.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuth0UserService auth0UserService;

        public UserController(IUserService userService, IAuth0UserService auth0UserService)
        {
            this.userService = userService;
            this.auth0UserService = auth0UserService;
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
            var auth0Result = await auth0UserService.CreateAuth0UserAsync(user);
            if (auth0Result.success)
            {
                string userId = auth0Result.content;
                userId = userId.Substring(1,30);
                user.UserId = userId;
                var dbResult = await userService.CreateUserAsync(user);
                if (dbResult.success)
                {
                    TempData["success"] = dbResult.content;
                    return RedirectToAction("Index");
                }
                else TempData["error"] = dbResult.content;
            }
            else TempData["error"] = "User could not be created";
            return View(user);


            //var dbResult = await userService.CreateUserAsync(user);
            //if (dbResult.success)
            //{
            //    TempData["success"] = dbResult.content;
            //    var auth0Result = await auth0UserService.CreateAuth0UserAsync(user);
            //    if (auth0Result.success)
            //    {
            //        TempData["success"] = auth0Result.content;
            //        return RedirectToAction("Index");
            //    }
            //    else TempData["error"] = auth0Result.content;
            //}
            //else TempData["error"] = dbResult.content;

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
            var dbResult = await userService.DeleteUserAsync(user);
            if (dbResult.success)
            {
                TempData["success"] = dbResult.content;
                var auth0Result = await auth0UserService.DeleteAuth0UserAsync(user);
                if (auth0Result.success)
                {
                    TempData["success"] = auth0Result.content;
                    return RedirectToAction("Index");
                }
                else TempData["error"] = auth0Result.content;
            }
            else TempData["error"] = dbResult.content;

            return RedirectToAction("Index");
        }


        #endregion

    }

}
