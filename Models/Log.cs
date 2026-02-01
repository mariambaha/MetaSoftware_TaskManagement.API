namespace MetaSoftware_TaskManagement.API.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int UserId { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
    }

}
