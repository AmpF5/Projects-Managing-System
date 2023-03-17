using System.ComponentModel.DataAnnotations;

namespace ProjectsManagingSystem.Models.Project;

public class ProjectDto
{
    [Required]
    [MaxLength(25)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime Deadline { get; set; }
}