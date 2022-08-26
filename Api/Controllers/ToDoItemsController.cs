using Application.TodoItems.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetToDoItemsAsync()
        {
            var response = await Mediator.Send(new GetToDoItemsQuery());
            return Ok(response);
        }
    }
}
