using Microsoft.EntityFrameworkCore;
using MetaSoftware_TaskManagement.API.Data;
using MetaSoftware_TaskManagement.API.Models;
using MetaSoftware_TaskManagement.API.DTO;

namespace MetaSoftware_TaskManagement.API.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context) => _context = context;

        public async Task CreateTask(int userId, TaskDto dto, string ipAddress)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                UserId = userId
            };
            _context.Tasks.Add(task);

            _context.Logs.Add(new Log
            {
                UserId = userId,
                Action = $"Created task {dto.Title}",
                IPAddress = ipAddress
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskItem>> GetTasks(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}
