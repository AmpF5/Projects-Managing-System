using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProjectsManagingSystem.Entities;

namespace ProjectsManagingSystem.ExtensionMethods;

public static class TestExtenions
{
    public static IIncludableQueryable<Project, Member> IncludeMembers(this DbSet<Project> query)
    {
        return query.Include(p => p.MemberProjects).ThenInclude(p => p.Member);
    }
    public static IIncludableQueryable<Member, Project> IncludeProjects(this DbSet<Member> query)
    {
        return query.Include(p => p.MemberProjects).ThenInclude(p => p.Project);
    }
}