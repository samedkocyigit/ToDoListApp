using ToDoListApp.Models;

namespace ToDoListApp.DTOs
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public TaskState? State { get; set; }
    }
}
