using ToDoListApp.Domain.Models.Models;

namespace ToDoListApp.Domain.Models.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public TaskState State { get; set; }
    }
}
