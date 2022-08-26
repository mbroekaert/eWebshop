using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ITodoItemsService toDoItemsService;
        
        public ToDoItemsController(ITodoItemsService toDoItemsService)
        {
            this.toDoItemsService = toDoItemsService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await toDoItemsService.GetTodosAsync());
        }
    }
}
