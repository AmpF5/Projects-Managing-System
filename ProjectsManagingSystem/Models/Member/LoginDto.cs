using System.ComponentModel.DataAnnotations;

namespace ProjectsManagingSystem.Models.Member
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
