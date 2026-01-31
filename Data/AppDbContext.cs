using Microsoft.EntityFrameworkCore;
using MetaSoftware_TaskManagement.API.Models;

namespace MetaSoftware_TaskManagement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
