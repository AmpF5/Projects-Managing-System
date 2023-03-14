using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.ProjectTask;

namespace ProjectsManagingSystem.Models.Project;

public class ProjectResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<ProjectTaskResponseDto> Tasks { get; set; }
    public List<MemberResponseDto> Members { get; set; }
    public DateTime DateOfCreation { get; set; }
    
}