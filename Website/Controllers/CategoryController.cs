using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private const string BaseUrl = "https://localhost:7060/Api/Category";
        private readonly ApplicationHttpClient _client = new ApplicationHttpClient(BaseUrl);

        public async Task<IActionResult> Index()
        {
                var httpResponse = await _client.GetAsync(BaseUrl);
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                var category = JsonSerializer.Deserialize<CategoryResponseDto[]>(responseAsString);
                return View(category);
        }
        #region Create new category
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> Create(Category category)
        {
                var content = JsonSerializer.Serialize(category);
                var httpResponse = await _client.PostAsync(BaseUrl, new StringContent(content, Encoding.Default, "application/json"));
                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            

            return View(category);
        }
        #endregion

        #region Update category
        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var httpResponse = await _client.GetAsync($"{BaseUrl}/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonSerializer.Deserialize<CategoryResponseDto>(responseAsString);
            return View(category);
        }

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var httpResponse = await _client.PutAsync($"{BaseUrl}/{category.Id}", new StringContent(content, Encoding.Default, "application/json"));
            return RedirectToAction("Index");
        }

        #endregion

    }

}
