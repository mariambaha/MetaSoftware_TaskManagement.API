using System.ComponentModel.DataAnnotations;

namespace MetaSoftware_TaskManagement.API.DTO
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
