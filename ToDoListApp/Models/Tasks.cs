namespace ToDoListApp.Models
{
    public enum TaskState
    {
        Task,
        InProgress,
        Completed
    }
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; } = TaskState.Task;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
