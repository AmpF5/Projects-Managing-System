using ProjectsManagingSystem.Entities;

namespace ProjectsManagingSystem.Models;

public class ProjectResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<ProjectTaskResponseDto> Tasks { get; set; }
    public List<Member> Members { get; set; }
    public DateTime DateOfCreation { get; set; }
    
}