using TodoList.Data.Context;

namespace TodoList.Service
{
    public class TodoService
    {
        private AppDbContext _context;
        public TodoService(AppDbContext context)
        {
            _context = context;
        }
    }
}
