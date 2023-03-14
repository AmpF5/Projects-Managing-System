namespace ProjectsManagingSystem.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<ProjectTask> Tasks { get; set; }
    public List<Member> Members { get; set; }
    public DateTime DateOfCreation { get; set; } = DateTime.Now;
}