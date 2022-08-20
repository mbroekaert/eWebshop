using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;
using Website.Models;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        /* to do : set BaseUrl in appsettings*/
        private const string BaseUrl = "https://localhost:7060/Api/User";
        private readonly ApplicationHttpClient _client = new ApplicationHttpClient(BaseUrl);

        #region Get categories
        public async Task<IActionResult> Index()
        {
            var httpResponse = await _client.GetAsync(BaseUrl);
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserResponseDto[]>(responseAsString);
            return View(user);
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
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _client.PostAsync(BaseUrl, new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                TempData["success"] = "User created successfully";
                return RedirectToAction("Index");
            }
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();


            return View(user);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var httpResponse = await _client.GetAsync($"{BaseUrl}/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserResponseDto>(responseAsString);
            return View(user);
        }

        // POST
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _client.PutAsync($"{BaseUrl}/{user.Id}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                TempData["success"] = "User updated successfully";
            }
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete category

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var httpResponse = await _client.GetAsync($"{BaseUrl}/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserResponseDto>(responseAsString);
            return View(user);
        }
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            var httpResponse = await _client.DeleteAsync($"{BaseUrl}/{user.Id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                TempData["success"] = "User deleted successfully";
            }
            return RedirectToAction("Index");
        }


        #endregion

    }

}
