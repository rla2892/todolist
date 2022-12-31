using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Model.ViewModel;
using TodoList.Service;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoService _service;
        public TodoController(TodoService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult AddTodo([FromBody] TodoViewAdd todo)
        {
            _service.AddTodo(todo);
            return Ok();
        }
    }
}
