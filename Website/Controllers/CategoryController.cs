using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Website.Models;
using Website.Models.Dto;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        private const string BaseUrl = "https://localhost:7060/Api/Category";
        private readonly ApplicationHttpClient _client = new ApplicationHttpClient(BaseUrl);

        public async Task<ActionResult> Index()
        {
                var httpResponse = await _client.GetAsync(BaseUrl);
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                var category = JsonSerializer.Deserialize<CategoryListDto>(responseAsString);
                return View(category.Categories);
        }
        #region Create new category
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> Create(Category category)
        {
           // if (ModelState.IsValid)
            //{
                var content = JsonSerializer.Serialize(category);
                var httpResponse = await _client.PostAsync(BaseUrl, new StringContent(content, Encoding.Default, "application/json"));
                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            //}

            return View(category);
        }
        #endregion
        #region Edit category

        public IActionResult Edit ()
        {
            return View();
        }

        public async Task<ActionResult> Edit(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var httpResponse = await _client.PutAsync(BaseUrl, new StringContent(content,Encoding.Default, "application/json"));
            return RedirectToAction("Index");
        }

        #endregion

    }

}
