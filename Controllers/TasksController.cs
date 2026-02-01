using MetaSoftware_TaskManagement.API.Data;
using MetaSoftware_TaskManagement.API.DTO;
using MetaSoftware_TaskManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetaSoftware_TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // جلب كل المهام للمستخدم
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            if (!HttpContext.Items.ContainsKey("UserId"))
                return Unauthorized("User not authenticated");

            int userId = (int)HttpContext.Items["UserId"];
            var tasks = await _context.Tasks
                                      .Where(t => t.UserId == userId)
                                      .ToListAsync();

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!HttpContext.Items.ContainsKey("UserId"))
                return Unauthorized("User not authenticated");

            int userId = (int)HttpContext.Items["UserId"];
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            var task = new TaskItem
            {
                Title = dto.Title.Trim(),
                Description = dto.Description?.Trim(),
                Status = string.IsNullOrEmpty(dto.Status) ? "Pending" : dto.Status,
                UserId = userId
            };

            _context.Tasks.Add(task);

            var log = new Log
            {
                UserId = userId,
                Action = $"Created Task: {task.Title}",
                IPAddress = ipAddress
            };
            _context.Logs.Add(log);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Task created successfully", taskId = task.Id });
        }
    }
}