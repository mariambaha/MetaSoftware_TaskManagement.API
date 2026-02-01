using System.ComponentModel.DataAnnotations;

namespace MetaSoftware_TaskManagement.API.DTO
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }

}
