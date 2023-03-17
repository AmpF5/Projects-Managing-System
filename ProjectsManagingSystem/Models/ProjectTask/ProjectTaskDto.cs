using System.ComponentModel.DataAnnotations;
using ProjectsManagingSystem.Entities;

namespace ProjectsManagingSystem.Models.ProjectTask;

public class ProjectTaskDto
{
    [Required]
    [MaxLength(25)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public DateTime Deadline { get; set; }
    public DateTime DateOfCreation { get;} = DateTime.Now;
    public int? MemberId{ get; set; }
    public int ProjectId { get; set; }
    public State State { get;} = State.ToDo;

}