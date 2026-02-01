using Microsoft.EntityFrameworkCore;
using MetaSoftware_TaskManagement.API.Data;
using MetaSoftware_TaskManagement.API.Models;
using MetaSoftware_TaskManagement.API.DTO;

namespace MetaSoftware_TaskManagement.API.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTask(int userId, TaskDto dto, string ipAddress)
        {
            var task = new TaskItem
            {
                Title = dto.Title.Trim(),
                Description = dto.Description?.Trim(),
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pending" : dto.Status,
                UserId = userId
            };

            _context.Tasks.Add(task);

            _context.Logs.Add(new Log
            {
                UserId = userId,
                Action = $"Created task: {task.Title}",
                IPAddress = ipAddress
            });

            await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task<List<TaskItem>> GetTasks(int userId)
        {
            return await _context.Tasks
                                 .Where(t => t.UserId == userId)
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
