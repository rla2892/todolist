using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TodoList.Data.Model;
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

        [HttpPost]
        public IActionResult AddTodo([FromBody] TodoViewAdd todo)
        {
            _service.AddTodo(todo);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult ModifyTodo(Guid id, [FromBody] TodoViewModify todo)
        {
            _service.ModifyTodo(id, todo);
            return Ok();
        }

        [HttpPut("done/{id}")]
        public IActionResult SetDone(Guid id)
        {
            _service.SetDone(id, true);
            return Ok();
        }

        [HttpPut("undo/{id}")]
        public IActionResult Undo(Guid id)
        {
            _service.SetDone(id, false);
            return Ok();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var list = _service.ListTodo();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var todo = _service.GetTodo(id);

            return Ok(todo);
        }
    }
}
