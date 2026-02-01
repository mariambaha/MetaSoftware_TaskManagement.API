using System.ComponentModel.DataAnnotations;

namespace MetaSoftware_TaskManagement.API.DTO
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
