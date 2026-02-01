using MetaSoftware_TaskManagement.API.DTO;
using MetaSoftware_TaskManagement.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace MetaSoftware_TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            if (!HttpContext.Items.ContainsKey("UserId"))
                return Unauthorized("User not authenticated");

            int userId = (int)HttpContext.Items["UserId"];

            var tasks = await _taskService.GetTasks(userId);
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

            var taskId = await _taskService.CreateTask(userId, dto, ipAddress);

            return Ok(new
            {
                message = "Task created successfully",
                taskId
            });
        }
    }
}
