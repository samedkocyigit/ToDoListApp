using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Domain.Models.Models;
using ToDoListApp.Exceptions;
using ToDoListApp.Infrastructure.Repositories.GenericRepository;


namespace ToDoListApp.Infrastructure.Repositories.TasksRepository
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
            var tasks = await _context.Tasks
                           .Where(t => t.UserId == userId)
                           .ToListAsync();
            if (tasks.Count == 0)
                throw new ErrorExceptions("There is no task with that userId ");



            return tasks;
        }
        public async Task UpdateTaskStateAsync(int taskId, string state)
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
                    throw new ErrorExceptions("There is no state with that name: "+ state);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
