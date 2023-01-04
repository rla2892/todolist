using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TodoList.Data.Model;
using TodoList.Data.Model.ViewModel;
using TodoList.Service;

namespace TodoList.Controllers
{
    [Route("api/todo")] // NOTE : api url path 에 소문자 이용하기 위해 [controller] 사용 안함
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

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
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
