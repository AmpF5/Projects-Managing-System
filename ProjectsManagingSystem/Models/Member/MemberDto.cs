using System.ComponentModel.DataAnnotations;

namespace ProjectsManagingSystem.Models.Member;

public class MemberDto
{
 
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword{ get; set; }
}