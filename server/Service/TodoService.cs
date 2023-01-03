using System;
using System.Collections.Generic;
using System.Linq;
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

        private Todo getTodo(Guid id) => _context.Todo.FirstOrDefault(o => o.Id == id);

        public void ModifyTodo(Guid id, TodoViewModify newTodo)
        {
            var now = DateTime.Now;
            var todo = getTodo(id);
            if (todo != null)
            {
                todo.Content = newTodo.Content;
                todo.UpdatedTime = now;
                if(newTodo.IsDone)
                {
                    todo.DoneTime = now;
                }
                else
                {
                    todo.DoneTime = null;
                }

                _context.SaveChanges();
            }
        }
        public void SetDone(Guid id, bool isDone)
        {
            var now = DateTime.Now;
            var todo = getTodo(id);
            if (todo != null)
            {
                todo.UpdatedTime = now;
                if (isDone)
                {
                    todo.DoneTime = now;
                }
                else
                {
                    todo.DoneTime = null;
                }

                _context.SaveChanges();
            }
        }

        public List<Todo> ListTodo() => _context.Todo.ToList();
        public Todo GetTodo(Guid id) => getTodo(id);
    }
}
