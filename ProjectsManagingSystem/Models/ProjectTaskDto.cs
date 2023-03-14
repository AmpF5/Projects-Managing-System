using ProjectsManagingSystem.Entities;

namespace ProjectsManagingSystem.Models;

public class ProjectTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime DateOfCreation { get;} = DateTime.Now;
    public int? MemberId{ get; set; }
    public int ProjectId { get; set; }
    public State State { get;} = State.ToDo;

}