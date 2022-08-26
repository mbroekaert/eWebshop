using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace Website.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ITodoItemsService toDoItemsService;
        
        public ToDoItemsController(ITodoItemsService toDoItemsService)
        {
            this.toDoItemsService = toDoItemsService;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View((await toDoItemsService.GetTodosAsync()).ToPagedList(pageNumber, pageSize));
        }
    }
}
