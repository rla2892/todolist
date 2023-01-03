using System;

namespace TodoList.Data.Model.ViewModel
{
    public class TodoViewAdd
    {
        public string Content { get; set; }
    }

    public class TodoViewModify
    {
        public string Content { get; set; }
        public bool IsDone { get; set; }
    }
}