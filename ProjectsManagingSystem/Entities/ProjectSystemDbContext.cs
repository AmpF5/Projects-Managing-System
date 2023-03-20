using Microsoft.EntityFrameworkCore;

namespace ProjectsManagingSystem.Entities;

public class ProjectSystemDbContext : DbContext
{
    public ProjectSystemDbContext(DbContextOptions<ProjectSystemDbContext> options) : base(options)
    {
        
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<MemberProject> MemberProjects { get; set; }
  
}