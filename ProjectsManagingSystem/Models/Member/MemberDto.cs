using System.ComponentModel.DataAnnotations;

namespace ProjectsManagingSystem.Models.Member;

public class MemberDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}