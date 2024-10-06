using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.DTOs;
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
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _tasksService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
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
        public async Task<ActionResult<TaskDto>> AddTask(CreateTaskDto createTaskDto)
        {
            if (createTaskDto == null || createTaskDto.UserId <= 0)
            {
                return BadRequest("Task or UserId Cannot be Null");
            }

            var taskDto = await _tasksService.AddTaskAsync(createTaskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskDto.Id }, taskDto);
        }        
        
        //// PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            var oldTask = await _tasksService.GetTaskByIdAsync(id);
            if (oldTask == null)
            {
                return BadRequest();
            }
            if (id != updateTaskDto.Id)
            {
                return BadRequest();
            }
            var updatedTask =await _tasksService.UpdateTaskAsync(updateTaskDto);
            return Ok(updatedTask);
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

        
        [HttpPut("{taskId}/{state}")]
        public async Task<IActionResult> UpdateTaskState(int taskId, string state)
        {
            try
            {
                await _tasksService.UpdateTaskStateAsync(taskId, state);
                return Ok("Task state updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("amına koycam ya");
            }
        }
    }
}
