using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;
        private readonly IUserService _userService;

        public TasksController(ITasksService tasksService, IUserService userService)
        {
            _tasksService = tasksService;
            _userService = userService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasks()
        {
            var tasks = await _tasksService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTaskById(int id)
        {
            var task = await _tasksService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<Tasks>> AddTask(Tasks task)
        {
            if(task == null || task.UserId<=0)
            {
                return BadRequest("Task or UserId Cannot be Null");
            }

            ////var user= await _userService.GetUserByIdAsync(task.UserId);
            //if (user == null)
            //{
            //    return NotFound("User not Found");
            //}

            //task.User = user;
            await _tasksService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tasks task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            await _tasksService.UpdateTaskAsync(task);
            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _tasksService.DeleteTaskAsync(id);
            return NoContent();
        }
        // GET: api/Tasks/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasksByUserId(int userId)
        {
            var tasks = await _tasksService.GetTasksByUserIdAsync(userId);

            return Ok(tasks); // Bulunan görevleri döner
        }
    }
}
