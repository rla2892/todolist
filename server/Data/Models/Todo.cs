using System;

namespace TodoList.Data.Model
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? DoneTime { get; set; }
        public bool IsDone
        {
            get => this.DoneTime.HasValue;
        }
    }
}