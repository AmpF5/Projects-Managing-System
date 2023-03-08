using Microsoft.EntityFrameworkCore;

namespace ProjectsManagingSystem.Entities;

public class ProjectSystemDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-ALLUHR9;Database=ProjectsManagingSystemDb;Trusted_Connection=True;Encrypt=False;");
    }
}