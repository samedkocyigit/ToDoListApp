using ToDoListApp.Data.Repositories.Abstract;
using ToDoListApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ToDoListApp.Data.Repositories.Concrete
{
    public class TasksRepository : GenericRepository<Tasks>, ITasksRepository
    {
        private readonly ToDoAppContext _context;
        public TasksRepository(ToDoAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tasks>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                           .Where(t => t.UserId == userId)
                           .ToListAsync();
        }
        public async Task UpdateTaskStateAsync(int taskId,string state)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                if (state == "InProgress")
                {
                    task.State = TaskState.InProgress;
                }
                else if (state == "Task")
                {
                    task.State = TaskState.Created;
                }
                else if (state == "Completed")
                {
                    task.State = TaskState.Completed;
                }
                else
                {
                }
               await _context.SaveChangesAsync();
            }
        }
    }
}
