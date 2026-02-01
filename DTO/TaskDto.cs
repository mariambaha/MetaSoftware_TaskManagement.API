using System.ComponentModel.DataAnnotations;

namespace MetaSoftware_TaskManagement.API.DTO
{
    public class TaskDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
    }

}
