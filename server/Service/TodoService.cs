using System;
using System.Net.WebSockets;
using TodoList.Data.Context;
using TodoList.Data.Model;
using TodoList.Data.Model.ViewModel;

namespace TodoList.Service
{
    public class TodoService
    {
        private AppDbContext _context;
        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public void AddTodo(TodoViewAdd todo)
        {
            var now = DateTime.Now;
            var newTodo = new Todo()
            {
                Id= Guid.NewGuid(),
                Content= todo.Content,
                CreatedTime= now,
                UpdatedTime= now,
                DoneTime=null
            };
            _context.Todo.Add(newTodo);
            _context.SaveChanges(); // TODO: async 동작 확인
        }
    }
}
