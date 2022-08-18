﻿using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;
using Website.Models;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        /* to do : set BaseUrl in appsettings*/
        private const string BaseUrl = "https://localhost:7060/Api/Category";
        private readonly ApplicationHttpClient _client = new ApplicationHttpClient(BaseUrl);

        #region Get categories
        public async Task<IActionResult> Index()
        {
                var httpResponse = await _client.GetAsync(BaseUrl);
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                var category = JsonSerializer.Deserialize<CategoryResponseDto[]>(responseAsString);
                return View(category);
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
                var content = JsonSerializer.Serialize(category);
                var httpResponse = await _client.PostAsync(BaseUrl, new StringContent(content, Encoding.Default, "application/json"));
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            

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

        // POST
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var httpResponse = await _client.PutAsync($"{BaseUrl}/{category.Id}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                TempData["success"] = "Category updated successfully";
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
            var category = JsonSerializer.Deserialize<CategoryResponseDto>(responseAsString);
            return View(category);
        }
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            var httpResponse = await _client.DeleteAsync($"{BaseUrl}/{category.Id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                TempData["success"] = "Category deleted successfully";
            }
            return RedirectToAction("Index");
        }


        #endregion

    }

}
